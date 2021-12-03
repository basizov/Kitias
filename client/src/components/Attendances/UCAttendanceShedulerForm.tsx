import React, {useEffect} from 'react';
import {Form, Formik} from "formik";
import {useTypedSelector} from "../../hooks/useTypedSelector";
import {useDispatch} from "react-redux";
import {
  getSubjects,
  getSubjectsNames
} from "../../store/subjectStore/asyncActions";
import {
  Button, ButtonGroup,
  FormControl,
  Grid, IconButton,
  InputLabel, List, ListItem, ListItemText,
  MenuItem,
  Select, styled,
  TextField,
  Typography
} from "@mui/material";
import {
  createSheduler, deleteSheduler,
  getGroupsNames,
  getGroupStudents, updateSheduler
} from "../../store/attendanceStore/asyncActions";
import {attendanceActions} from "../../store/attendanceStore";
import {CreateAttendanceType} from "../../model/Attendance/CreateAttendanceModel";
import {ShedulerListType} from "../../model/Attendance/ShedulerList";
import {Loading} from "../../layout/Loading";
import {Delete} from "@mui/icons-material";

const StyledList = styled(List)(({theme}) => ({
  height: '9rem',
  overflowY: 'auto',
  position: 'relative',
  borderRadius: '.3rem',
  border: `.3rem solid ${theme.palette.primary.main}`
}));

type PropsType = {
  attendace: ShedulerListType | null;
  close: () => void;
  isUpdating?: boolean;
};

export const UCAttendanceShedulerForm: React.FC<PropsType> = ({
                                                                attendace,
                                                                close,
                                                                isUpdating = false
                                                              }) => {
  const dispatch = useDispatch();
  const {
    loadingHelper: loadingSubject,
    subjectsNames,
    subjects
  } = useTypedSelector(s => s.subject);
  const {
    loadingHelper,
    groupsNames,
    groupStudents,
    shedulerGroup
  } = useTypedSelector(s => s.attendance);

  useEffect(() => {
    dispatch(getSubjectsNames());
    dispatch(getGroupsNames());
  }, [dispatch]);

  if (loadingHelper || loadingSubject) {
    return <Loading loading={loadingHelper || loadingSubject}/>;
  }
  return (
    <Formik
      initialValues={{
        selectedSubject: attendace ? attendace.subjectName : '' as string,
        selectedGroup: attendace ? shedulerGroup : '' as string,
        name: attendace ? attendace.name : '' as string,
        newStudent: '' as string
      } as const}
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
        if (isUpdating) {
          await dispatch(updateSheduler(attendace!.id,
            {
              groupNumber: values.selectedGroup,
              subjectName: values.selectedSubject,
              name: values.name
            }, attendances));
        } else {
          await dispatch(createSheduler({
            groupNumber: values.selectedGroup,
            subjectName: values.selectedSubject,
            name: values.name
          }, attendances));
        }
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
                  <ListItem key={gs} disablePadding sx={{
                    position: 'relative'
                  }}>
                    <ListItemText primary={gs} sx={{
                      textAlign: 'center',
                      margin: '0 .5rem'
                    }}/>
                    <IconButton
                      color='error'
                      onClick={() => {
                        dispatch(attendanceActions.setGroupStudents([
                          ...groupStudents.filter(g => g !== gs)
                        ]));
                      }}
                    ><Delete/></IconButton>
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
                <ButtonGroup
                  sx={{marginLeft: 'auto', marginTop: '.3rem'}}
                  size={`${isUpdating ? 'small' : 'medium'}`}
                  variant={`${isUpdating ? 'outlined' : 'text'}`}
                >
                  <Button
                    type='submit'
                  >{isUpdating ? ' Обновить журнал' : ' Добавить журнал'}</Button>
                  {isUpdating && <Button
                      color='error'
                      onClick={async () => {
                        await dispatch(deleteSheduler(attendace!.id));
                        close();
                      }}
                  >Удалить</Button>}
                </ButtonGroup>
              </Grid>
            </Grid>
          </Grid>
        </Form>
      )}
    </Formik>
  );
};