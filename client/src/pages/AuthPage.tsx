import React from 'react';
import {Link} from "react-router-dom";

export const AuthPage: React.FC = () => {
  return (
    <div>
      Auth
      <Link to='/'>Go to home</Link>
    </div>
  );
};
