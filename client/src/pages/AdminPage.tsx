import React, {useEffect, useState} from 'react';
import {useDispatch} from "react-redux";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {getTeachersShedulers} from "../store/attendanceStore/asyncActions";
import {Loading} from "../layout/Loading";
import {
  Card,
  CardHeader,
  Divider,
  Grid,
  IconButton, useMediaQuery
} from "@mui/material";
import {MoreHoriz} from "@mui/icons-material";
import {TotalResult} from "../components/Attendances/TotalResult";

export const AdminPage: React.FC = () => {
  const dispatch = useDispatch();
  const isMobile = useMediaQuery('(min-width: 560px)');
  const {
    teacherShedulers,
    loadingInitial
  } = useTypedSelector(s => s.attendance);
  const [selectedSheduler, setSelectedSheduler] = useState('');

  useEffect(() => {
    dispatch(getTeachersShedulers());
  }, [dispatch]);

  if (loadingInitial) {
    return <Loading loading={loadingInitial}/>;
  }
  return (
    <Grid
      container
      spacing={1}
    >
      <Grid item xs={12} sx={{
        maxHeight: isMobile ? '15rem' : '7rem',
        overflowY: 'auto'
      }}>
        <Grid container spacing={1}>
          {teacherShedulers.map(sheduler => (
            <Grid
              item
              xs={12}
              sm={6}
              md={4}
              key={sheduler.id}
            >
              <Card>
                <CardHeader
                  title={sheduler.subjectName}
                  subheader={`${sheduler.fullName} - ${sheduler.name}`}
                  action={<IconButton onClick={(e) => {
                    e.preventDefault();
                    setSelectedSheduler(sheduler.id);
                  }}><MoreHoriz/></IconButton>}
                />
                <Divider/>
              </Card>
            </Grid>
          ))}
        </Grid>
      </Grid>
      {selectedSheduler && <Grid item xs={12}>
          <TotalResult id={selectedSheduler} canChangeGrade={false}/>
      </Grid>}
    </Grid>
  );
};
