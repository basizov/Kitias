import React, {useState} from 'react';
import {
  Button, ButtonGroup,
  Grid,
  useMediaQuery
} from "@mui/material";
import {useDispatch} from "react-redux";
import {deleteGroup, editStudents} from "../../store/groupStore/asyncActions";
import {EditStudents} from "./EditStudents";

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
      <EditStudents
        id='newStudent'
        newStudent={newStudent}
        setNewStudent={(value) => setNewStudent(value)}
        students={students}
        setStudents={(value) => setStudents(value)}
      />
      <ButtonGroup
        size='small'
        sx={{marginLeft: 'auto'}}
      >
        <Button
          onClick={async () => {
            await dispatch(editStudents(
              id,
              students.map(s => s.id === '' ? s.fullName : s.id))
            );
            close();
          }}
        >Редактировать</Button>
        <Button
          color='error'
          onClick={async () => {
            await dispatch(deleteGroup(id));
            close();
          }}
        >Удалить</Button>
      </ButtonGroup>
    </Grid>
  );
};
