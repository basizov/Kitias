import React, {createContext, useEffect, useMemo, useState} from 'react';
import {Sidebar} from "../components/Sidebar";
import {
  Backdrop,
  CircularProgress,
  createTheme, CssBaseline,
  IconButton, Paper, styled,
  ThemeProvider,
  useMediaQuery
} from "@mui/material";
import {useDispatch} from "react-redux";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {ColorEnums, defaultActions} from "../store/defaultStore";
import {Brightness4, Brightness7} from "@mui/icons-material";
import {PrivateRoute} from "./PrivateRoute";
import {Routes, Route} from "react-router-dom";
import {AuthPage} from "../pages/AuthPage";
import {PublicRoute} from "./PublicRoute";
import {isAuthAsync} from "../store/defaultStore/asyncActions";
import {ShedulersPage} from "../pages/ShedulersPage";
import {AttendancesPage} from "../pages/AttendancesPage";
import {SubjectsPage} from "../pages/SubjectsPage";
import {CalendarPage} from "../pages/CalendarPage";
import {SignUpPage} from "../pages/SignUpPage";

const RootPaper = styled(Paper)({
  position: 'absolute',
  top: 0,
  left: 0,
  width: '100%',
  height: '100vh',
  borderRadius: 0
});

const StyledIconButton = styled(IconButton)(({theme}) => ({
  position: 'fixed',
  bottom: ".3rem",
  right: ".3rem",
  zIndex: 10001,
  [theme.breakpoints.down('sm')]: {
    top: '.5rem',
    right: '1rem',
    bottom: 'auto'
  }
}));

export const ColorModeContext = createContext({
  toggleColorMode: () => {
  }
});

const Main = styled('main', {shouldForwardProp: (prop) => prop !== 'open'})<{
    open?: boolean;
  }>(({theme, open}) => ({
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
    }),
    [theme.breakpoints.down('sm')]: {
      marginLeft: 0,
      marginTop: `calc(${theme.spacing(7)} + 1px)`,
      height: 'auto'
    }
  }))
;

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
    },
    breakpoints: {
      values: {
        xs: 0,
        sm: 620,
        md: 900,
        lg: 1200,
        xl: 1536,
      }
    },
    components: {
      MuiCssBaseline: {
        styleOverrides: {
          body: {
            scrollbarColor: "#6b6b6b #2b2b2b",
            "&::-webkit-scrollbar, & *::-webkit-scrollbar": {
              backgroundColor: "transparent",
              height: 7,
              width: 7
            },
            "&::-webkit-scrollbar-thumb, & *::-webkit-scrollbar-thumb": {
              borderRadius: 8,
              backgroundColor: "#6b6b6b"
            },
            "&::-webkit-scrollbar-thumb:focus, & *::-webkit-scrollbar-thumb:focus": {
              backgroundColor: "#959595",
            },
            "&::-webkit-scrollbar-thumb:active, & *::-webkit-scrollbar-thumb:active": {
              backgroundColor: "#959595",
            },
            "&::-webkit-scrollbar-thumb:hover, & *::-webkit-scrollbar-thumb:hover": {
              backgroundColor: "#959595",
            },
            "&::-webkit-scrollbar-corner, & *::-webkit-scrollbar-corner": {
              backgroundColor: "#2b2b2b",
            },
          }
        }
      }
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
        <CssBaseline/>
        <RootPaper>
          <Backdrop
            sx={{color: '#fff'}}
            open={loadingInitial}
          ><CircularProgress color="inherit"/></Backdrop>
          {!loadingInitial && <React.Fragment>
              <Sidebar open={open} setOpen={setOpen}/>
              <Main open={open} sx={{height: 'calc(100% - 16px)'}}>
                  <Routes>
                      <Route path='/' element={<PrivateRoute>
                        <CalendarPage/>
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
                      <Route path='/register' element={<PublicRoute>
                        <SignUpPage/>
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
