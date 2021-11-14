import React from 'react';
import {Button, Paper, styled} from "@mui/material";
import {defaultActions} from "../store/defaultStore";
import {useDispatch} from "react-redux";

const AuthPaper = styled(Paper)({
  position: 'absolute',
  top: 0,
  left: 0,
  width: '100%',
  minHeight: '100vh',
  borderRadius: 0,
  zIndex: 10000,
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center'
});

export const AuthPage: React.FC = () => {
  const dispatch = useDispatch();

  return (
    <AuthPaper>
      <Button
        onClick={() => dispatch(defaultActions.setIsAuth(true))}
      >Войти</Button>
    </AuthPaper>
  );
};
