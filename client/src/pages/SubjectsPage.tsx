import React, {useEffect, useState} from 'react';
import {
  Card,
  CardContent,
  CardHeader, CircularProgress, Divider,
  Grid, IconButton, List, ListItem, ListItemText, Modal, Paper, styled,
  Typography
} from "@mui/material";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {
  getSubjects,
  getSubjectsInfos
} from "../store/subjectStore/asyncActions";
import {useDispatch} from "react-redux";
import {Loading} from "../layout/Loading";
import {MoreHoriz} from "@mui/icons-material";

const StyledPaper = styled(Paper)({
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  padding: '1rem'
});

export const SubjectsPage: React.FC = () => {
  const dispatch = useDispatch();
  const [open, setOpen] = useState(false);
  const {
    subjectsInfos,
    loading,
    loadingInitial,
    subjects
  } = useTypedSelector(s => s.subject);

  useEffect(() => {
    dispatch(getSubjectsInfos());
  }, [dispatch]);

  if (loadingInitial) {
    return <Loading loading={loadingInitial}/>;
  }
  return (
    <Grid container direction='column'>
      <Grid item>
        <Typography
          variant="h5"
          component="div"
          sx={{marginLeft: '.7rem'}}
        >Ваши предметы</Typography>
      </Grid>
      <Grid
        container
        spacing={1}
      >
        {Object.entries(subjectsInfos).map(([key, value]) => (
          <Grid item xs={12} sm={6} md={4} key={key}>
            <Card>
              <CardHeader
                sx={{padding: '.7rem', paddingBottom: '.3rem'}}
                title={key}
                action={<IconButton onClick={async (e) => {
                  e.preventDefault();
                  setOpen(true);
                  await dispatch(getSubjects(key));
                }}><MoreHoriz/></IconButton>}
              />
              <Divider/>
              <CardContent sx={{padding: '.7rem', paddingTop: '.3rem'}}>
                <Grid container spacing={1}>
                  {Object.entries(value).map(([vKey, vValue]) => (
                    <Grid item xs={4} sm={6} md={4} key={vKey}>
                      <Typography
                        variant='subtitle1'
                        color="text.primary"
                        align='center'
                      >{vKey}</Typography>
                      {[].map.call(vValue, date => (
                        <Typography
                          key={date}
                          variant="body2"
                          align='center'
                          color="text.secondary"
                        >{date}</Typography>
                      ))}
                    </Grid>
                  ))}
                </Grid>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>
      <Modal
        open={open}
        onClose={() => setOpen(false)}
      >
        <StyledPaper>
          {loading ? <CircularProgress color="inherit"/> :
            <List>
              {subjects.map(subject => (
                <ListItem disablePadding key={subject.id}>
                  <ListItemText primary={subject.name}/>
                </ListItem>
              ))}
            </List>}
        </StyledPaper>
      </Modal>
    </Grid>
  );
};
