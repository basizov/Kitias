import React, {useCallback, useEffect, useState} from 'react';
import {
  Grid,
  styled,
  Tab,
  Tabs
} from "@mui/material";
import {AttendancesTable} from "../components/Attendances/AttendancesTable";
import {
  getAttendances,
  getAttendanceSubjects, getGrades, getSheduler, getShedulerSAttendaces
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
  const [valueTab, setValueTab] = useState(0);
  const {loadingInitial} = useTypedSelector(s => s.attendance);
  const {subjects} = useTypedSelector(s => s.subject);

  useEffect(() => {
    if (params.id) {
      dispatch(getAttendances(params.id));
      dispatch(getSheduler(params.id));
      dispatch(getAttendanceSubjects(params.id));
    }
  }, [params.id, params.subjectName, dispatch]);

  const checkLen = useCallback((subjectType: string) => {
    const typeSubjects = subjects
      .filter(s => s.type === subjectType);

    return typeSubjects.filter(s => s.isGiveScore);
  }, [subjects]);

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
        onChange={async (_, selectedTab) => {
          const lecCheck = checkLen('Лекция').length === 0;
          const pracCheck = checkLen('Практика').length === 0;
          const labCheck = checkLen('Лабораторная работа').length === 0;
          const check = [
            lecCheck,
            pracCheck,
            labCheck
          ].filter(s => s).length;

          if (selectedTab === 6 - check && params.id) {
            await dispatch(getGrades());
            await dispatch(getShedulerSAttendaces(params.id));
          }
          if (check === 1 && selectedTab > 4) {
            setValueTab(selectedTab + 1);
          } else if (check === 2 && selectedTab > 3) {
            setValueTab(selectedTab + 2);
          } else if (check === 3 && selectedTab > 2) {
            setValueTab(selectedTab + 3);
          } else {
            setValueTab(selectedTab);
          }
          setTab(selectedTab);
        }}
        variant='scrollable'
        scrollButtons='auto'
        sx={{maxWidth: '100%'}}
      >
        <StyledTab label='Посещений лекций'/>
        <StyledTab label='Посещение практик'/>
        <StyledTab label='Посещение лаб'/>
        {checkLen('Лекция').length !== 0 &&
        <StyledTab label='Работы по лекциям'/>}
        {checkLen('Практика').length !== 0 &&
        <StyledTab label='Работы по практикам'/>}
        {checkLen('Лабораторная работа').length !== 0 &&
        <StyledTab label='Работы по лабам'/>}
        <StyledTab label='Итог'/>
      </Tabs>
      <Grid item>
        {valueTab === 0 && <AttendancesTable subjectType='Лекция'/>}
        {valueTab === 1 && <AttendancesTable subjectType='Практика'/>}
        {valueTab === 2 &&
        <AttendancesTable subjectType='Лабораторная работа'/>}
        {valueTab === 3 && <AttendancesTable
            subjectType='Лекция'
            withScore={true}
        />}
        {valueTab === 4 && <AttendancesTable
            subjectType='Практика'
            withScore={true}
        />}
        {valueTab === 5 && <AttendancesTable
            subjectType='Лабораторная работа'
            withScore={true}
        />}
        {valueTab === 6 && <TotalResult/>}
      </Grid>
    </Grid>
  );
};
