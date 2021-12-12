import React, {useMemo, useState} from 'react';
import {
  Box, Button, ButtonGroup,
  CircularProgress,
  Grid, IconButton, TextField,
  Typography, useMediaQuery
} from "@mui/material";
import {SubjectType} from "../../model/Subject/Subject";
import {Check, Delete, RotateLeft} from "@mui/icons-material";
import {useDispatch} from "react-redux";
import {
  deleteSubject, deleteSubjectByName,
  updateSubjectsNames
} from "../../store/subjectStore/asyncActions";
import {ModalHoc} from "../HOC/ModalHoc";
import {UpdateSubject} from "./UpdateSubject";
import {Form, Formik} from "formik";
import {exportSheduler} from "../../store/attendanceStore/asyncActions";
import {object, string} from "yup/es";
import {SchemaOptions} from "yup/es/schema";

export type PropsType = {
  name: string;
  setName: (name: string) => void;
  loading: boolean;
  subjects: SubjectType[];
  close: () => void;
};

export const SubjectsInfos: React.FC<PropsType> = ({
                                                     loading,
                                                     subjects,
                                                     name,
                                                     setName,
                                                     close
                                                   }) => {
  const dispatch = useDispatch();
  const [updateOpen, setUpdateOpen] = useState(false);
  const isTablet = useMediaQuery('(min-width: 450px)');
  const [selectedSubject, setSelectedSubject] = useState<SubjectType | null>(null);

  const initialState = useMemo(() => ({
    name: name
  } as const), [name]);

  const validationSchema: SchemaOptions<typeof initialState> = useMemo(() => {
    return object({
      name: string().required()
    });
  }, []);

  if (loading) {
    return <CircularProgress color='inherit'/>;
  }
  return (
    <Grid container>
      <Grid item xs={12}>
        <Formik
          initialValues={initialState}
          validationSchema={validationSchema}
          onSubmit={async (values) => {
            setName(values.name);
            await dispatch(updateSubjectsNames(name, values.name));
          }}
        >
          {({
              values,
              handleSubmit,
              handleBlur,
              handleChange,
              errors
            }) => (
            <Form onSubmit={handleSubmit}>
              <Grid container sx={{position: 'relative'}}>
                <TextField
                  id="name"
                  type="text"
                  variant="standard"
                  fullWidth
                  onBlur={handleBlur}
                  value={values.name}
                  onChange={handleChange}
                  onFocus={(e) => e.target.select()}
                  error={!!errors.name}
                  label="Наименование предмета"
                />
                <IconButton
                  type='submit'
                  size='small'
                  sx={{
                    position: 'absolute',
                    top: '50%',
                    right: 0,
                    transform: 'translateY(-50%)'
                  }}
                ><Check/></IconButton>
              </Grid>
            </Form>
          )}
        </Formik>
      </Grid>
      <Grid container sx={{
        maxWidth: isTablet ? '35rem' : '17rem',
        maxHeight: '10rem',
        overflowY: 'auto'
      }}>
        {subjects.map((subject) => (
          <Grid
            container
            key={subject.id}
            justifyContent='space-between'
            alignItems='center'
          >
            <Grid item sx={{maxWidth: '67%'}}>
              <Typography
                variant='subtitle1'
                component='div'
              >
                <Box>{`${subject.type}. `}</Box>
                <Box
                  sx={{
                    whiteSpace: 'nowrap',
                    textOverflow: 'ellipsis',
                    overflow: 'hidden'
                  }}
                >{`${subject.theme || 'Нет темы'}`}</Box>
                <Box>{`${subject.date}`}</Box>
              </Typography>
            </Grid>
            <Grid item>
              <Box>
                <IconButton
                  color='warning'
                  onClick={() => {
                    setSelectedSubject(subject);
                    setUpdateOpen(true);
                  }}
                ><RotateLeft/></IconButton>
                <IconButton
                  color='error'
                  onClick={() => dispatch(deleteSubject(subject.id))}
                ><Delete/></IconButton>
              </Box>
            </Grid>
          </Grid>
        ))}
      </Grid>
      <ButtonGroup
        sx={{marginLeft: 'auto', marginTop: '.3rem'}}
        size='small'
      >
        <Button
          color='success'
          onClick={async () => {
            await dispatch(exportSheduler(name));
            close();
          }}
        >Экспортировать</Button>
        <Button
          color='error'
          onClick={async () => {
            await dispatch(deleteSubjectByName(name));
            close();
          }}
        >Удалить</Button>
      </ButtonGroup>
      <ModalHoc
        open={updateOpen}
        onClose={() => setUpdateOpen(false)}
      ><UpdateSubject
        subject={selectedSubject!}
        close={() => setUpdateOpen(false)}
      /></ModalHoc>
    </Grid>
  );
};
