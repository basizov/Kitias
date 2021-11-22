import React, {createContext, useEffect, useMemo, useState} from 'react';
import {Sidebar} from "../components/Sidebar";
import {
  Backdrop,
  CircularProgress,
  createTheme,
  IconButton, Paper, styled,
  ThemeProvider,
  useMediaQuery
} from "@mui/material";
import {useDispatch} from "react-redux";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {ColorEnums, defaultActions} from "../store/defaultStore";
import {Brightness4, Brightness7} from "@mui/icons-material";
import {PrivateRoute} from "./PrivateRoute";
import {HomePage} from "../pages/HomePage";
import {Routes, Route} from "react-router-dom";
import {AuthPage} from "../pages/AuthPage";
import {PublicRoute} from "./PublicRoute";
import {isAuthAsync} from "../store/defaultStore/asyncActions";
import {ShedulersPage} from "../pages/ShedulersPage";
import {AttendancesPage} from "../pages/AttendancesPage";
import {SubjectsPage} from "../pages/SubjectsPage";

const RootPaper = styled(Paper)({
  position: 'absolute',
  top: 0,
  left: 0,
  width: '100%',
  minHeight: '100vh',
  borderRadius: 0
});

const StyledIconButton = styled(IconButton)({
  position: 'fixed',
  bottom: ".3rem",
  right: ".3rem",
  zIndex: 10001
});

export const ColorModeContext = createContext({
  toggleColorMode: () => {
  }
});

const Main = styled('main', { shouldForwardProp: (prop) => prop !== 'open' })<{
  open?: boolean;
}>(({ theme, open }) => ({
  flexGrow: 1,
  padding: theme.spacing(1),
  transition: theme.transitions.create('margin', {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  marginLeft: theme.spacing(7),
  ...(open && {
    transition: theme.transitions.create('margin', {
      easing: theme.transitions.easing.easeOut,
      duration: theme.transitions.duration.enteringScreen,
    }),
    marginLeft: 300,
  }),
}));

export const App: React.FC = () => {
  const dispatch = useDispatch();
  const {colorTheme, loadingInitial} = useTypedSelector(s => s.common);
  const preferDarkMode = useMediaQuery('(prefers-color-scheme: dark)');
  const changeColorTheme = useMemo(() => ({
    toggleColorMode: () => {
      if (colorTheme === ColorEnums.LIGHT_COLOR) {
        dispatch(defaultActions.setDarkTheme());
      } else {
        dispatch(defaultActions.setLightTheme());
      }
    }
  }), [colorTheme, dispatch]);
  const theme = useMemo(() => createTheme({
    palette: {
      mode: colorTheme
    }
  }), [colorTheme]);
  const [open, setOpen] = useState(false);

  useEffect(() => {
    dispatch(isAuthAsync());
  }, [dispatch]);
  useEffect(() => {
    if (preferDarkMode) {
      dispatch(defaultActions.setDarkTheme());
    } else {
      dispatch(defaultActions.setLightTheme());
    }
  }, [preferDarkMode, dispatch]);

  return (
    <ColorModeContext.Provider value={changeColorTheme}>
      <ThemeProvider theme={theme}>
        <RootPaper>
          <Backdrop
            sx={{color: '#fff'}}
            open={loadingInitial}
          ><CircularProgress color="inherit"/></Backdrop>
          {!loadingInitial && <React.Fragment>
              <Sidebar open={open} setOpen={setOpen}/>
              <Main open={open}>
                  <Routes>
                      <Route path='/' element={<PrivateRoute>
                        <HomePage/>
                      </PrivateRoute>}/>
                      <Route path='/subjects' element={<PrivateRoute>
                        <SubjectsPage/>
                      </PrivateRoute>}/>
                      <Route path='/attendances/:id' element={<PrivateRoute>
                        <AttendancesPage/>
                      </PrivateRoute>}/>
                      <Route path='/attendances' element={<PrivateRoute>
                        <ShedulersPage/>
                      </PrivateRoute>}/>
                      <Route path='/login' element={<PublicRoute>
                        <AuthPage/>
                      </PublicRoute>}/>
                  </Routes>
              </Main>
              <StyledIconButton
                  color="inherit"
                  onClick={changeColorTheme.toggleColorMode}
              >{theme.palette.mode === ColorEnums.LIGHT_COLOR ?
                <Brightness7/> :
                <Brightness4/>}</StyledIconButton>
          </React.Fragment>}
        </RootPaper>
      </ThemeProvider>
    </ColorModeContext.Provider>
  );
};
