import React from 'react';
import {Link} from "react-router-dom";

export const HomePage: React.FC = () => {
  return (
    <div>
      Home
      <Link to='/login'>Go to login</Link>
    </div>
  );
};
