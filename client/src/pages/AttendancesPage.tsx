import React, {useState} from 'react';
import {Grid, styled, Tab, Tabs} from "@mui/material";
import {AttendanceTable} from "../components/Attendances/AttendanceTable";
import {AttendancesTable} from "../components/Attendances/AttendancesTable";

const StyledTab = styled(Tab)({
  fontSize: '.7rem'
});

export const AttendancesPage: React.FC = () => {
  const [tab, setTab] = useState(0);

  return (
    <Grid
      container
      direction='column'
      spacing={1}
    >
      <Tabs value={tab} onChange={(_, selectedTab) => setTab(selectedTab)}>
        <StyledTab label='Посещений лекций'/>
        <StyledTab label='Посещение практик'/>
        <StyledTab label='Посещение лаб'/>
        <StyledTab label='Выполнение работ по лекциям'/>
        <StyledTab label='Выполнение работ по практикам'/>
        <StyledTab label='Выполнение работ по лабам'/>
        <StyledTab label='Итог'/>
      </Tabs>
      <Grid item>
        {tab === 0 && <AttendancesTable subjectType='Лекция'/>}
        {tab === 1 && <AttendancesTable subjectType='Практика'/>}
        {tab === 6 && <AttendanceTable/>}
      </Grid>
    </Grid>
  );
};
