import React, {useState} from 'react';
import {
  Box,
  CircularProgress,
  Grid, IconButton,
  Typography
} from "@mui/material";
import {SubjectType} from "../../model/Subject/Subject";
import {Delete, RotateLeft} from "@mui/icons-material";
import {useDispatch} from "react-redux";
import {deleteSubject} from "../../store/subjectStore/asyncActions";
import {ModalHoc} from "../HOC/ModalHoc";
import {UpdateSubject} from "./UpdateSubject";

export type PropsType = {
  loading: boolean;
  subjects: SubjectType[];
};

export const SubjectsInfos: React.FC<PropsType> = ({
                                                     loading,
                                                     subjects
                                                   }) => {
  const dispatch = useDispatch();
  const [updateOpen, setUpdateOpen] = useState(false);
  const [selectedSubject, setSelectedSubject] = useState<SubjectType | null>(null);

  if (loading) {
    return <CircularProgress color='inherit'/>;
  }
  return (
    <Grid container sx={{
      maxHeight: '15rem',
      overflowY: 'auto'
    }}>
      {subjects.map((subject, i) => (
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
      <ModalHoc
        open={updateOpen}
        onClose={() => setUpdateOpen(false)}
      ><UpdateSubject subject={selectedSubject!}/></ModalHoc>
    </Grid>
  );
};
