import React, {createContext, useEffect, useMemo} from 'react';
import {Sidebar} from "../components/Sidebar";
import {
  Box,
  createTheme,
  Grid,
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
import {AttendancesPage} from "../pages/AttendancesPage";

const RootPaper = styled(Paper)({
  position: 'absolute',
  top: 0,
  left: 0,
  width: '100%',
  minHeight: '100vh',
  borderRadius: 0
});

const StyledIconButton = styled(IconButton)({
  position: 'absolute',
  bottom: ".3rem",
  right: ".3rem",
  zIndex: 10001
});

export const ColorModeContext = createContext({
  toggleColorMode: () => {
  }
});

export const App: React.FC = () => {
  const dispatch = useDispatch();
  const {colorTheme} = useTypedSelector(s => s.common);
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
          <Grid container>
            <Sidebar/>
            <Box component="main" sx={{flexGrow: 1, p: 1}}>
              <Routes>
                <Route path='/' element={<PrivateRoute>
                  <HomePage/>
                </PrivateRoute>}/>
                <Route path='/attendances' element={<PrivateRoute>
                  <AttendancesPage/>
                </PrivateRoute>}/>
                <Route path='/login' element={<PublicRoute>
                  <AuthPage/>
                </PublicRoute>}/>
              </Routes>
            </Box>
            <StyledIconButton
              color="inherit"
              onClick={changeColorTheme.toggleColorMode}
            >{theme.palette.mode === ColorEnums.LIGHT_COLOR ?
              <Brightness7/> :
              <Brightness4/>}</StyledIconButton>
          </Grid>
        </RootPaper>
      </ThemeProvider>
    </ColorModeContext.Provider>
  );
};
