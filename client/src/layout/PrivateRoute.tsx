import {Navigate} from 'react-router-dom';
import {useTypedSelector} from "../hooks/useTypedSelector";
import React from "react";

export const PrivateRoute: React.FC = ({children}) => {
  const {isAuth} = useTypedSelector(s => s.common);

  return <>{isAuth ? children : <Navigate to="/login"/>}</>;
};
