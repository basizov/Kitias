import React from 'react';
import {Box, Grid, IconButton, TextField, Typography} from "@mui/material";
import {Add, Delete} from "@mui/icons-material";
import {StudentInGroupType} from "../../model/Group/GroupModel";

type PropsType = {
  id: string;
  newStudent: string;
  setNewStudent: (value: string) => void;
  students: StudentInGroupType[];
  setStudents: (newStudents: StudentInGroupType[]) => void;
};

export const EditStudents: React.FC<PropsType> = ({
                                                    id,
                                                    newStudent,
                                                    setNewStudent,
                                                    students,
                                                    setStudents
                                                  }) => {
  return (
    <React.Fragment>
      <Grid item xs={12} sx={{position: 'relative'}}>
        <TextField
          id={id}
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
            if (newStudent !== '') {
              setStudents([
                {
                  id: '',
                  fullName: newStudent,
                },
                ...students
              ]);
              setNewStudent('');
            }
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
    </React.Fragment>
  );
};
