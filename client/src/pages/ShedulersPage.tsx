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
  getShedulers
} from "../store/attendanceStore/asyncActions";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {Groups} from "@mui/icons-material";
import {Loading} from "../layout/Loading";
import {getSubjects} from "../store/subjectStore/asyncActions";

export const ShedulersPage: React.FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const {shedulers, loading} = useTypedSelector(s => s.attendance);

  useEffect(() => {
    dispatch(getShedulers());
  }, [dispatch]);

  if (loading) {
    return <Loading loading={loading}/>;
  }
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
          sx={{marginLeft: '.7rem'}}
        >Ваши группы</Typography>
      </Grid>
      <Grid item>
        <List sx={{padding: 0}}>
          {shedulers.map((s) => (
            <ListItem key={s.id} disablePadding>
              <ListItemButton
                onClick={async () => {
                  navigate(`${s.id}/${s.subjectName}`);
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
    </Grid>
  );
};
