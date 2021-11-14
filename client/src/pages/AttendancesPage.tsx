import React from 'react';
import {Grid} from "@mui/material";
import {AttendanceTable} from "../components/Attendances/AttendanceTable";

export const AttendancesPage: React.FC = () => {
  return (
    <Grid
      container
      direction='column'
      spacing={1}
    >
      <Grid item>
        <AttendanceTable/>
      </Grid>
    </Grid>
  );
};
