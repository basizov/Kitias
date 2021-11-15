import React, {useEffect} from 'react';
import {
  Grid,
  List,
  ListItem, ListItemButton,
  ListItemIcon,
  ListItemText, Typography
} from "@mui/material";
import {useDispatch} from "react-redux";
import {useNavigate} from "react-router-dom";
import {
  getAttendances,
  getShedulers
} from "../store/attendanceStore/asyncActions";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {Groups} from "@mui/icons-material";

export const ShedulersPage: React.FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const {shedulers} = useTypedSelector(s => s.attendance);

  useEffect(() => {
    dispatch(getShedulers());
  }, [dispatch]);

  return (
    <Grid
      container
      direction='column'
      spacing={1}
    >
      <Grid item>
        <Typography
          variant="h5"
          component="div"
          sx={{paddingLeft: '.7rem'}}
        >Ваши группы</Typography>
      </Grid>
      <Grid item>
        <List sx={{padding: 0}}>
          {shedulers.map((s) => (
            <ListItem key={s.id} disablePadding>
              <ListItemButton
                onClick={async () => {
                  await dispatch(getAttendances(s.id));
                  navigate(`${s.id}`);
                }}
              >
                <ListItemIcon>
                  <Groups/>
                </ListItemIcon>
                <ListItemText primary={s.groupNumber}/>
              </ListItemButton>
            </ListItem>
          ))}
        </List>
      </Grid>
    </Grid>
  );
};
