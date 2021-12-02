import React, {useEffect} from 'react';
import {Form, Formik} from "formik";
import {useTypedSelector} from "../../hooks/useTypedSelector";
import {useDispatch} from "react-redux";
import {
  getSubjects,
  getSubjectsNames
} from "../../store/subjectStore/asyncActions";
import {
  Button,
  FormControl,
  Grid,
  InputLabel, List, ListItem, ListItemText,
  MenuItem,
  Select, styled,
  TextField,
  Typography
} from "@mui/material";
import {
  createSheduler,
  getGroupsNames,
  getGroupStudents
} from "../../store/attendanceStore/asyncActions";
import {attendanceActions} from "../../store/attendanceStore";
import {CreateAttendanceType} from "../../model/Attendance/CreateAttendanceModel";

const StyledList = styled(List)(({theme}) => ({
  height: '7rem',
  overflowY: 'auto',
  position: 'relative',
  borderRadius: '.3rem',
  border: `.3rem solid ${theme.palette.primary.main}`
}));

type PropsType = {
  close: () => void;
};

export const CreateAttendanceSheduler: React.FC<PropsType> = ({close}) => {
  const dispatch = useDispatch();
  const {
    subjectsNames,
    subjects
  } = useTypedSelector(s => s.subject);
  const {
    groupsNames,
    groupStudents
  } = useTypedSelector(s => s.attendance);

  useEffect(() => {
    dispatch(getSubjectsNames());
    dispatch(getGroupsNames());
    dispatch(attendanceActions.setGroupStudents([]));
  }, [dispatch]);

  return (
    <Formik
      initialValues={{
        selectedSubject: '',
        selectedGroup: '',
        name: '',
        newStudent: ''
      }}
      onSubmit={async (values) => {
        let attendances = [] as CreateAttendanceType[];

        subjects.forEach(s => {
          groupStudents.forEach(gs => {
            attendances.push({
              studentName: gs,
              subjectId: s.id
            });
          });
        });
        await dispatch(createSheduler({
          groupNumber: values.selectedGroup,
          subjectName: values.selectedSubject,
          name: values.name
        }, attendances));
        close();
      }}
    >
      {({
          handleSubmit,
          handleChange,
          handleBlur,
          values,
          errors,
          setFieldValue
        }) => (
        <Form onSubmit={handleSubmit}>
          <Grid container sx={{minWidth: '35rem'}} spacing={1}>
            <Grid item xs={4}>
              <TextField
                id="name"
                type="text"
                variant="outlined"
                fullWidth
                onBlur={handleBlur}
                value={values.name}
                onChange={handleChange}
                onFocus={(e) => e.target.select()}
                error={!!errors.name}
                label="Название журнала"
              />
            </Grid>
            <Grid item xs={4}>
              <FormControl fullWidth>
                <InputLabel id="selectedSubject-label"
                >Предмет</InputLabel>
                <Select
                  id="selectedSubject"
                  labelId="selectedSubject-label"
                  value={values.selectedSubject}
                  label="Предмет"
                  error={!!errors.selectedSubject}
                  onChange={async (e) => {
                    setFieldValue('selectedSubject', e.target.value);
                    await dispatch(getSubjects(e.target.value));
                  }}
                >
                  {subjectsNames.map(s => (
                    <MenuItem
                      key={s}
                      value={s}
                    >{s}</MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={4}>
              <FormControl fullWidth>
                <InputLabel id="selectedGroup-label"
                >Группа</InputLabel>
                <Select
                  id="selectedGroup"
                  labelId="selectedGroup-label"
                  value={values.selectedGroup}
                  label="Группа"
                  error={!!errors.selectedGroup}
                  onChange={async (e) => {
                    setFieldValue('selectedGroup', e.target.value);
                    await dispatch(getGroupStudents(e.target.value));
                  }}
                >
                  {groupsNames.map(g => (
                    <MenuItem
                      key={g.id}
                      value={g.id}
                    >{g.number}</MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={6}>
              <StyledList>
                {groupStudents.length > 0 && groupStudents.map(gs => (
                  <ListItem key={gs} disablePadding>
                    <ListItemText primary={gs} sx={{
                      textAlign: 'center',
                      margin: '0 .5rem',

                    }}/>
                  </ListItem>
                ))}
                {groupStudents.length === 0 && <Typography
                    sx={{
                      position: 'absolute',
                      top: '50%',
                      left: '50%',
                      transform: 'translate(-50%, -50%)'
                    }}
                >...Студенты...</Typography>}
              </StyledList>
            </Grid>
            <Grid item xs={6}>
              <Grid container>
                <Grid item xs={12}>
                  <TextField
                    id="newStudent"
                    type="text"
                    variant="outlined"
                    fullWidth
                    onBlur={handleBlur}
                    value={values.newStudent}
                    onChange={handleChange}
                    onFocus={(e) => e.target.select()}
                    error={!!errors.newStudent}
                    label="Новый студент"
                  />
                </Grid>
                <Button
                  variant='outlined'
                  sx={{marginLeft: 'auto', marginTop: '.3rem'}}
                  onClick={() => {
                    dispatch(attendanceActions.setGroupStudents([
                      values.newStudent,
                      ...groupStudents
                    ]));
                    setFieldValue('newStudent', '');
                  }}
                >Добавить студента</Button>
                <Button
                  type='submit'
                  sx={{marginLeft: 'auto', marginTop: '.3rem'}}
                >Добавить журнал</Button>
              </Grid>
            </Grid>
          </Grid>
        </Form>
      )}
    </Formik>
  );
};
