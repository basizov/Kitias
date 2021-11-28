import React, {useEffect, useState} from 'react';
import {
  Grid,
  styled,
  Tab,
  Tabs
} from "@mui/material";
import {AttendanceTable} from "../components/Attendances/AttendanceTable";
import {AttendancesTable} from "../components/Attendances/AttendancesTable";
import {
  getAttendances,
  getAttendanceSubjects
} from "../store/attendanceStore/asyncActions";
import {useDispatch} from "react-redux";
import {useParams} from "react-router";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {Loading} from "../layout/Loading";

const StyledTab = styled(Tab)({
  fontSize: '.7rem'
});

export const AttendancesPage: React.FC = () => {
  const dispatch = useDispatch();
  const params = useParams();
  const [tab, setTab] = useState(0);
  const {loading} = useTypedSelector(s => s.attendance);

  useEffect(() => {
    if (params.id) {
      dispatch(getAttendances(params.id));
      dispatch(getAttendanceSubjects(params.id));
    }
  }, [params.id, params.subjectName, dispatch]);

  if (loading) {
    return <Loading loading={loading}/>;
  }
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
