import React, {useEffect, useState} from 'react';
import {
  Button,
  Grid, IconButton,
  List,
  ListItem, ListItemButton,
  ListItemIcon,
  ListItemText, Typography, useMediaQuery
} from "@mui/material";
import {useDispatch} from "react-redux";
import {useNavigate} from "react-router-dom";
import {
  getShedulers, getShedulerStudentsGroup
} from "../store/attendanceStore/asyncActions";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {MoreHoriz, Apps} from "@mui/icons-material";
import {Loading} from "../layout/Loading";
import {ModalHoc} from "../components/HOC/ModalHoc";
import {UCAttendanceShedulerForm} from "../components/Attendances/UCAttendanceShedulerForm";
import {attendanceActions} from "../store/attendanceStore";
import {ShedulerListType} from "../model/Attendance/ShedulerList";
import {getSubjects} from "../store/subjectStore/asyncActions";

export const ShedulersPage: React.FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [openCreate, setOpenCreate] = useState(false);
  const [openUpdate, setOpenUpdate] = useState(false);
  const [selectedSheduler, setSelectedSheduler] = useState<ShedulerListType | null>(null);
  const {shedulers, loadingInitial} = useTypedSelector(s => s.attendance);
  const isMobile = useMediaQuery('(min-width: 430px)');

  useEffect(() => {
    dispatch(getShedulers());
  }, [dispatch]);

  const closeCreate = async () => {
    await dispatch(getShedulers());
    setOpenCreate(false);
  };

  const closeUpdate = async () => {
    await dispatch(getShedulers());
    setOpenUpdate(false);
  };

  if (loadingInitial) {
    return <Loading loading={loadingInitial}/>;
  }
  return (
    <Grid
      container
      direction='column'
    >
      <Grid container>
        <Grid item>
          <Typography
            variant="h5"
            component="div"
            sx={{marginLeft: '.7rem'}}
          >Журналы посещений</Typography>
        </Grid>
        <Button
          sx={{marginLeft: `${isMobile ? 'auto' : '.3rem'}`}}
          onClick={() => {
            dispatch(attendanceActions.setGroupStudents([]));
            setOpenCreate(true);
          }}
        >Добавить журнал</Button>
      </Grid>
      <Grid item>
        <List sx={{padding: 0}}>
          <Grid container spacing={1}>
            {shedulers.map((s) => (
              <Grid item xs={12} sm={6} md={4} key={s.id}>
                <ListItem disablePadding sx={{position: 'relative'}}>
                  <ListItemButton
                    onClick={async () => {
                      navigate(`${s.id}`);
                      dispatch(attendanceActions.setSelectedSheduler(s.name));
                    }}
                  >
                    <ListItemIcon>
                      <Apps/>
                    </ListItemIcon>
                    <ListItemText primary={s.name}/>
                  </ListItemButton>
                  <IconButton
                    size='small'
                    sx={{
                      position: 'absolute',
                      top: '50%',
                      right: '.3rem',
                      transform: 'translateY(-50%)'
                    }}
                    onClick={async () => {
                      await dispatch(getSubjects(s.name));
                      await dispatch(getShedulerStudentsGroup(s.id));
                      setSelectedSheduler(s);
                      setOpenUpdate(true);
                    }}
                  ><MoreHoriz/></IconButton>
                </ListItem>
              </Grid>
            ))}
          </Grid>
        </List>
      </Grid>
      <ModalHoc
        open={openCreate}
        onClose={closeCreate}
      ><UCAttendanceShedulerForm
        close={closeCreate}
        attendace={null}
      /></ModalHoc>
      <ModalHoc
        open={openUpdate}
        onClose={closeUpdate}
      ><UCAttendanceShedulerForm
        attendace={selectedSheduler!}
        close={closeUpdate}
        isUpdating={true}
      /></ModalHoc>
    </Grid>
  );
};
