import React, {useState} from 'react';
import {
  Box, Button,
  CircularProgress,
  Grid, IconButton, TextField,
  Typography
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
  const [selectedSubject, setSelectedSubject] = useState<SubjectType | null>(null);

  if (loading) {
    return <CircularProgress color='inherit'/>;
  }
  return (
    <Grid container>
      <Grid item xs={12}>
        <Formik
          initialValues={{name: name}}
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
        maxHeight: '10rem',
        overflowY: 'auto'
      }}>
        {subjects.map((subject) => (
          <Grid container key={subject.id}>
            <Grid item>
              <Typography variant='subtitle1' component='div'>
                {subject.type}. {subject.theme || 'Нет темы'}
              </Typography>
              <Typography variant='subtitle2' component='div'>
                {subject.date}
              </Typography>
            </Grid>
            <Box sx={{marginLeft: 'auto'}}>
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
        ))}
      </Grid>
      <Button
        color='error'
        sx={{marginLeft: 'auto'}}
        onClick={async () => {
          await dispatch(deleteSubjectByName(name));
          close();
        }}
      >Удалить предмет</Button>
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