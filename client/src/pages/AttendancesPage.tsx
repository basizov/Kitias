import React, {useEffect, useState} from 'react';
import {
  Grid,
  styled,
  Tab,
  Tabs
} from "@mui/material";
import {AttendancesTable} from "../components/Attendances/AttendancesTable";
import {
  getAttendances,
  getAttendanceSubjects, getSheduler
} from "../store/attendanceStore/asyncActions";
import {useDispatch} from "react-redux";
import {useParams} from "react-router";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {Loading} from "../layout/Loading";
import {TotalResult} from "../components/Attendances/TotalResult";

const StyledTab = styled(Tab)({
  fontSize: '.7rem'
});

export const AttendancesPage: React.FC = () => {
  const dispatch = useDispatch();
  const params = useParams();
  const [tab, setTab] = useState(0);
  const {loadingInitial} = useTypedSelector(s => s.attendance);

  useEffect(() => {
    if (params.id) {
      dispatch(getAttendances(params.id));
      dispatch(getSheduler(params.id));
      dispatch(getAttendanceSubjects(params.id));
    }
  }, [params.id, params.subjectName, dispatch]);

  if (loadingInitial) {
    return <Loading loading={loadingInitial}/>;
  }
  return (
    <Grid
      container
      direction='column'
      spacing={1}
    >
      <Tabs
        value={tab}
        onChange={(_, selectedTab) => setTab(selectedTab)}
        variant='scrollable'
        scrollButtons='auto'
        sx={{maxWidth: '100%'}}
      >
        <StyledTab label='Посещений лекций'/>
        <StyledTab label='Посещение практик'/>
        <StyledTab label='Посещение лаб'/>
        <StyledTab label='Работы по лекциям'/>
        <StyledTab label='Работы по практикам'/>
        <StyledTab label='Работы по лабам'/>
        <StyledTab label='Итог'/>
      </Tabs>
      <Grid item>
        {tab === 0 && <AttendancesTable subjectType='Лекция'/>}
        {tab === 1 && <AttendancesTable subjectType='Практика'/>}
        {tab === 2 && <AttendancesTable subjectType='Лабораторная работа'/>}
        {tab === 3 && <AttendancesTable
            subjectType='Лекция'
            withScore={true}
        />}
        {tab === 4 && <AttendancesTable
            subjectType='Практика'
            withScore={true}
        />}
        {tab === 5 && <AttendancesTable
            subjectType='Лабораторная работа'
            withScore={true}
        />}
        {tab === 6 && <TotalResult/>}
      </Grid>
    </Grid>
  );
};
