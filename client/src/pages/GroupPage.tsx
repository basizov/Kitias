import React, {useCallback, useEffect, useMemo, useState} from 'react';
import {
  Button, ButtonGroup,
  Card, CardContent,
  CardHeader, Divider,
  Grid,
  IconButton, Typography
} from "@mui/material";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {Loading} from "../layout/Loading";
import {useDispatch} from "react-redux";
import {getGroups} from "../store/groupStore/asyncActions";
import {MoreHoriz} from "@mui/icons-material";
import {ModalHoc} from "../components/HOC/ModalHoc";
import {EditGroupStudents} from "../components/Group/EditGroupStudents";
import {StudentInGroupType} from "../model/Group/GroupModel";
import {CreateGroup} from "../components/Group/CreateGroup";

export const GroupPage: React.FC = () => {
  const dispatch = useDispatch();
  const [isEditStudent, setIsEditStudent] = useState(false);
  const [selectedCourse, setSelectedCourse] = useState(0);
  const [isCreateStudent, setIsCreateStudent] = useState(false);
  const [selectedStudents, setSelectedStudents] = useState<StudentInGroupType[]>([]);
  const [selectedId, setSelectedId] = useState('');
  const {
    groups,
    loadingInitial
  } = useTypedSelector(s => s.group);
  const courseGroups = useMemo(() => {
    if (selectedCourse === 0) {
      return groups;
    }
    return groups.filter(g => g.course === selectedCourse);
  }, [groups, selectedCourse]);

  useEffect(() => {
    dispatch(getGroups());
  }, [dispatch]);

  const closeEditModal = useCallback(() => {
    dispatch(getGroups());
    setIsEditStudent(false);
  }, [dispatch]);

  const closeCreateModal = useCallback(() => {
    dispatch(getGroups());
    setIsCreateStudent(false);
  }, [dispatch]);

  if (loadingInitial) {
    return <Loading loading={loadingInitial}/>;
  }
  return (
    <Grid
      container
      spacing={1}
    >
      <Grid item xs={12}>
        <Button
          size='small'
          variant='outlined'
          onClick={() => setIsCreateStudent(true)}
        >Добавить группу</Button>
      </Grid>
      <Grid item xs={12}>
        <ButtonGroup size='small'>
          <Button disabled>Курс:</Button>
          <Button
            onClick={() => setSelectedCourse(0)}
          >Все</Button>
          <Button
            onClick={() => setSelectedCourse(1)}
          >1</Button>
          <Button
            onClick={() => setSelectedCourse(2)}
          >2</Button>
          <Button
            onClick={() => setSelectedCourse(3)}
          >3</Button>
          <Button
            onClick={() => setSelectedCourse(4)}
          >4</Button>
        </ButtonGroup>
      </Grid>
      {courseGroups.map(group => (
        <Grid item key={group.id} xs={12} sm={6} md={4}>
          <Card>
            <CardHeader
              title={group.number}
              subheader={`${group.course} курс`}
              action={<IconButton onClick={(e) => {
                e.preventDefault();
                setSelectedId(group.id);
                setIsEditStudent(true);
                setSelectedStudents(group.students);
              }}><MoreHoriz/></IconButton>}
            />
            <Divider/>
            <CardContent sx={{padding: '.7rem', paddingTop: '.3rem'}}>
              <Typography
                variant='subtitle1'
                sx={{marginLeft: '.3rem'}}
              >Студенты:</Typography>
              <Grid container sx={{maxHeight: '10rem', overflowY: 'auto'}}>
                {group.students.map(student => (
                  <Grid item xs={12} key={student.id}>
                    <Typography
                      variant='subtitle2'
                      color="text.secondary"
                      sx={{marginLeft: '.3rem'}}
                    >{student.fullName}</Typography>
                  </Grid>
                ))}
              </Grid>
            </CardContent>
          </Card>
        </Grid>
      ))}
      <ModalHoc
        open={isEditStudent}
        onClose={closeEditModal}
      ><EditGroupStudents
        id={selectedId}
        students={selectedStudents.map(s => ({
          id: s.id,
          fullName: s.fullName
        }))}
        close={closeEditModal}
      /></ModalHoc>
      <ModalHoc
        open={isCreateStudent}
        onClose={closeCreateModal}
      ><CreateGroup close={closeCreateModal}/></ModalHoc>
    </Grid>
  );
};
