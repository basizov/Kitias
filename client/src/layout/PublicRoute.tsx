import {Navigate} from 'react-router-dom';
import {useTypedSelector} from "../hooks/useTypedSelector";
import React from "react";
import {useMediaQuery} from "@mui/material";

export const PublicRoute: React.FC = ({children}) => {
  const {isAuth} = useTypedSelector(s => s.common);
  const isTablet = useMediaQuery('(min-width: 900px)');

  return <>{!isAuth ? children : <Navigate to={isTablet ? '/' : '/subjects'}/>}</>;
};
