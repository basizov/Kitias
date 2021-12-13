import React, {useState} from 'react';
import {
  Box,
  Button,
  Grid,
  IconButton,
  TextField,
  Typography, useMediaQuery
} from "@mui/material";
import {useDispatch} from "react-redux";
import {editStudents} from "../../store/groupStore/asyncActions";
import {Add, Delete} from "@mui/icons-material";

type PropsType = {
  id: string;
  students: StudentListType[];
  close: () => void;
};

export type StudentListType = {
  id: string;
  fullName: string;
};

export const EditGroupStudents: React.FC<PropsType> = ({
                                                         id,
                                                         students: stdns,
                                                         close
                                                       }) => {
  const dispatch = useDispatch();
  const [students, setStudents] = useState<StudentListType[]>(stdns);
  const [newStudent, setNewStudent] = useState<string>('');
  const isMobile = useMediaQuery('(min-width: 450px)');

  return (
    <Grid container sx={{minWidth: `${isMobile ? '25rem' : '17rem'}`}}>
      <Grid item xs={12} sx={{position: 'relative'}}>
        <TextField
          id="newStudent"
          type="text"
          variant="outlined"
          fullWidth
          value={newStudent}
          onChange={(e) => setNewStudent(e.target.value)}
          onFocus={(e) => e.target.select()}
          label="Студент"
        />
        <IconButton
          size='small'
          sx={{
            position: 'absolute',
            right: '.3rem',
            top: '50%',
            transform: 'translateY(-50%)'
          }}
          onClick={() => {
            setStudents([
              {
                id: '',
                fullName: newStudent,
              },
              ...students
            ]);
            setNewStudent('');
          }}
        ><Add/></IconButton>
      </Grid>
      <Grid item xs={12}>
        <Box sx={{
          position: 'relative',
          display: 'flex',
          flexDirection: 'column',
          height: '10rem',
          overflowY: 'auto',
          padding: '.5rem'
        }}>
          {students.map(s => (
            <Box
              key={s.id === '' ? s.fullName : s.id}
              sx={{position: 'relative'}}
            >
              <Box
                sx={{
                  whiteSpace: 'nowrap',
                  textOverflow: 'ellipsis',
                  overflow: 'hidden',
                  maxWidth: '67%'
                }}
              >{s.fullName}</Box>
              <IconButton
                size='small'
                color='error'
                sx={{
                  position: 'absolute',
                  top: '50%',
                  right: '.3rem',
                  transform: 'translateY(-50%)'
                }}
                onClick={() => setStudents(
                  students.filter(st => st.id !== s.id)
                )}
              ><Delete/></IconButton>
            </Box>
          ))}
          {students.length === 0 && <Typography
              sx={{
                position: 'absolute',
                top: '50%',
                left: '50%',
                transform: 'translate(-50%)'
              }}
          >...Студенты...</Typography>}
        </Box>
      </Grid>
      <Button
        size='small'
        sx={{marginLeft: 'auto'}}
        onClick={async () => {
          await dispatch(editStudents(
            id,
            students.map(s => s.id === '' ? s.fullName : s.id))
          );
          close();
        }}
      >Редактировать</Button>
    </Grid>
  );
};
