import React, {useEffect, useState} from 'react';
import {
  Button,
  Grid,
  List,
  ListItem, ListItemButton,
  ListItemIcon,
  ListItemText, Typography
} from "@mui/material";
import {useDispatch} from "react-redux";
import {useNavigate} from "react-router-dom";
import {
  getShedulers
} from "../store/attendanceStore/asyncActions";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {Groups} from "@mui/icons-material";
import {Loading} from "../layout/Loading";
import {ModalHoc} from "../components/HOC/ModalHoc";
import {CreateAttendanceSheduler} from "../components/Attendances/CreateAttendanceSheduler";
import {attendanceActions} from "../store/attendanceStore";

export const ShedulersPage: React.FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [openCreate, setOpenCreate] = useState(false);
  const {shedulers, loadingInitial} = useTypedSelector(s => s.attendance);

  useEffect(() => {
    dispatch(getShedulers());
  }, [dispatch]);

  const closeCreate = async () => {
    await dispatch(getShedulers());
    setOpenCreate(false);
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
          sx={{marginLeft: 'auto'}}
          onClick={() => setOpenCreate(true)}
        >Добавить новый журнал</Button>
      </Grid>
      <Grid item>
        <List sx={{padding: 0}}>
          {shedulers.map((s) => (
            <ListItem key={s.id} disablePadding>
              <ListItemButton
                onClick={async () => {
                  navigate(`${s.id}`);
                  dispatch(attendanceActions.setSelectedSheduler(s.name));
                }}
              >
                <ListItemIcon>
                  <Groups/>
                </ListItemIcon>
                <ListItemText primary={s.name}/>
              </ListItemButton>
            </ListItem>
          ))}
        </List>
      </Grid>
      <ModalHoc
        open={openCreate}
        onClose={closeCreate}
      ><CreateAttendanceSheduler close={closeCreate}/></ModalHoc>
    </Grid>
  );
};
