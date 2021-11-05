import {Navigate, Route} from 'react-router-dom';
import {useTypedSelector} from "../hooks/useTypedSelector";
import React, {ReactElement} from "react";

type PropsType = {
  path: string;
  element: ReactElement;
};

export const PublicRoute: React.FC<PropsType> = ({path, element, ...rest}) => {
  const {isAuth} = useTypedSelector(s => s.common);
  const ele = !isAuth
    ? element
    : <Navigate to="/" replace state={{path}}/>;

  return <Route path={path} element={ele}/>;
};
