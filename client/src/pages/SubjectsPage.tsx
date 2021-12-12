import React, {useEffect, useState} from 'react';
import {
  Button,
  Card,
  CardContent,
  CardHeader, Divider,
  Grid, IconButton, Typography, useMediaQuery
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
import {format, parse} from "date-fns";

export const SubjectsPage: React.FC = () => {
  const dispatch = useDispatch();
  const [openInfo, setOpenInfo] = useState(false);
  const [name, setName] = useState('');
  const [openCreate, setOpenCreate] = useState(false);
  const isMobile = useMediaQuery('(min-width: 400px)');
  const {
    subjectsInfos,
    loading,
    loadingInitial,
    subjects
  } = useTypedSelector(s => s.subject);

  useEffect(() => {
    dispatch(getSubjectsInfos());
  }, [dispatch]);

  const closeCreateModel = async () => {
    await dispatch(getSubjectsInfos());
    setOpenCreate(false);
  };

  const closeInfoModel = async () => {
    await dispatch(getSubjectsInfos());
    setOpenInfo(false);
  };

  const chooseColor = (date: string) => {
    if (date === format(new Date(), 'dd.MM.yyyy')) {
      return '#cfcf25';
    } else if (parse(date, 'dd.MM.yyyy', new Date()) >
      parse(format(new Date(), 'dd.MM.yyyy'), 'dd.MM.yyyy', new Date())) {
      return '#a34e4e';
    }
    return '#2a8a21';
  };

  if (loadingInitial) {
    return <Loading loading={loadingInitial}/>;
  }
  return (
    <Grid container direction='column'>
      <Grid container>
        <Grid item xs={isMobile ? 6 : 12}>
          <Typography
            variant="h5"
            component="div"
            sx={{marginLeft: '.7rem'}}
          >Ваши предметы</Typography>
        </Grid>
        <Button
          sx={{marginLeft: `${isMobile ? 'auto' : '.3rem'}`}}
          onClick={() => setOpenCreate(true)}
        >Добавить предмет</Button>
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
                  setName(key);
                  await dispatch(getSubjects(key));
                }}><MoreHoriz/></IconButton>}
              />
              <Divider/>
              <CardContent sx={{padding: '.7rem', paddingTop: '.3rem'}}>
                <Grid container spacing={1}>
                  {Object.entries(value).map(([vKey, vValue]) => (
                    <Grid item container key={vKey}>
                      <Grid item xs={12}>
                        <Typography
                          variant='subtitle1'
                          color="text.primary"
                          sx={{marginLeft: '.3rem'}}
                        >{vKey}</Typography>
                      </Grid>
                      {[].map.call(vValue, (date: string) => (
                        <Grid item xs={6} md={4} key={date}>
                          <Typography
                            variant="body2"
                            align='center'
                            color={chooseColor(date.split(' ')[0])}
                          >{date}</Typography>
                        </Grid>
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
        onClose={closeInfoModel}
      ><SubjectsInfos
        name={name}
        setName={setName}
        loading={loading}
        subjects={subjects}
        close={closeInfoModel}
      /></ModalHoc>
      <ModalHoc
        open={openCreate}
        onClose={closeCreateModel}
      ><CreateSubject close={closeCreateModel}/></ModalHoc>
    </Grid>
  );
};
