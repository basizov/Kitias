import React, {useEffect, useState} from 'react';
import {
  Button,
  Card,
  CardContent,
  CardHeader, Divider,
  Grid, IconButton, Typography
} from "@mui/material";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {
  getSubjects,
  getSubjectsInfos
} from "../store/subjectStore/asyncActions";
import {useDispatch} from "react-redux";
import {Loading} from "../layout/Loading";
import {MoreHoriz} from "@mui/icons-material";
import {SubjectsInfos} from "../components/Subject/SubjectsInfos";
import {ModalHoc} from "../components/HOC/ModalHoc";
import {CreateSubject} from "../components/Subject/CreateSubject";

export const SubjectsPage: React.FC = () => {
  const dispatch = useDispatch();
  const [openInfo, setOpenInfo] = useState(false);
  const [openCreate, setOpenCreate] = useState(false);
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
      <Grid container>
        <Grid item xs={6}>
          <Typography
            variant="h5"
            component="div"
            sx={{marginLeft: '.7rem'}}
          >Ваши предметы</Typography>
        </Grid>
        <Button
          sx={{marginLeft: 'auto'}}
          onClick={() => setOpenCreate(true)}
        >Добавить новый предмет</Button>
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
                  setOpenInfo(true);
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
      <ModalHoc
        open={openInfo}
        onClose={() => setOpenInfo(false)}
      ><SubjectsInfos loading={loading} subjects={subjects}/></ModalHoc>
      <ModalHoc
        open={openCreate}
        onClose={() => setOpenCreate(false)}
      ><CreateSubject/></ModalHoc>
    </Grid>
  );
};
