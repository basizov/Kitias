import React, {useCallback, useEffect, useState} from 'react';
import {
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

export const GroupPage: React.FC = () => {
  const dispatch = useDispatch();
  const [idEditStudent, setIsEditStudent] = useState(false);
  const [selectedStudents, setSelectedStudents] = useState<StudentInGroupType[]>([]);
  const [selectedId, setSelectedId] = useState('');
  const {
    groups,
    loadingInitial
  } = useTypedSelector(s => s.group);

  useEffect(() => {
    dispatch(getGroups());
  }, [dispatch]);

  const closeEditModal = useCallback(() => {
    dispatch(getGroups());
    setIsEditStudent(prev => !prev);
  }, [dispatch]);

  if (loadingInitial) {
    return <Loading loading={loadingInitial}/>;
  }
  return (
    <Grid
      container
      spacing={1}
    >
      {groups.map(group => (
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
        open={idEditStudent}
        onClose={closeEditModal}
      ><EditGroupStudents
        id={selectedId}
        students={selectedStudents.map(s => ({
          id: s.id,
          fullName: s.fullName
        }))}
        close={closeEditModal}
      /></ModalHoc>
    </Grid>
  );
};
