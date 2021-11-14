import React from 'react';
import {defaultActions} from "../store/defaultStore";
import {Button} from "@mui/material";
import {useDispatch} from "react-redux";


export const HomePage: React.FC = () => {
  const dispatch = useDispatch();

  return (
    <section>
      <Button
        onClick={() => dispatch(defaultActions.setIsAuth(false))}
      >Go to login</Button>
    </section>
  );
};
