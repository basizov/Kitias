import React, {useMemo, useState} from 'react';
import {
  Button, ButtonGroup,
  Checkbox,
  FormControlLabel,
  Grid, List, ListItem, ListItemText,
  TextField
} from "@mui/material";
import {Form, Formik} from "formik";
import {CreateSubjectPairLecture} from "./CreateSubjectPairLecture";
import {CreateSubjectPairPractise} from "./CreateSubjectPairPractise";
import {CreateSubjectPairLaborotory} from "./CreateSubjectPairLaborotory";
import {CreateSubjectType} from "../../model/Subject/CreateSubjectModel";
import {createSubjects} from "../../store/subjectStore/asyncActions";
import {addDays, format} from "date-fns";
import {useDispatch} from "react-redux";

export const initialSubjectTypeState = {
  subjectName: '' as string,
  themes: false,
  newTheme: '' as string,
  themesList: [] as string[],
  lectureCount: 0 as number,
  lectureWeek: '' as string,
  lectureFirstDate: new Date(),
  practiseFirstDate: new Date(),
  laborotoryFirstDate: new Date(),
  lectureTime: new Date(),
  lectureDates: [] as Date[],
  practiseCount: 0 as number,
  practiseWeek: '' as string,
  practiseTime: new Date(),
  practiseDates: [] as Date[],
  laborotoryCount: 0 as number,
  laborotoryWeek: '' as string,
  laborotoryTime: new Date(),
  laborotoryDates: [] as Date[]
} as const;

type PropsType = {
  close: () => void;
};

export const CreateSubject: React.FC<PropsType> = ({close}) => {
  const dispatch = useDispatch();
  const [newSubjects, setNewSubjects] = useState<CreateSubjectType[]>([]);
  const Days = useMemo(() => [
    'Понедельник',
    'Вторник',
    'Среда',
    'Четверг',
    'Пятница',
    'Суббота',
    'Воскресенье'
  ], []);

  return (
    <Formik
      initialValues={initialSubjectTypeState}
      onSubmit={async (values) => {
        let subjects = newSubjects;

        for (let i = 0; i < values.lectureCount; ++i) {
          subjects = [
            ...subjects, {
              day: Days[values.lectureFirstDate.getDay()],
              date: format(
                values.lectureWeek === 'Еженедельно' ?
                  addDays(values.lectureFirstDate, 7 * i) :
                  addDays(values.lectureFirstDate, 14 * i),
                'dd.MM.yyy'
              ),
              name: values.subjectName,
              theme: values.newTheme,
              time: format(values.lectureTime, 'hh:mm:ss'),
              week: values.lectureWeek,
              type: 'Лекция'
            }
          ];
        }
        for (let i = 0; i < values.practiseCount; ++i) {
          subjects = [
            ...subjects, {
              day: Days[values.practiseFirstDate.getDay()],
              date: format(
                values.practiseWeek === 'Еженедельно' ?
                  addDays(values.practiseFirstDate, 7 * i) :
                  addDays(values.practiseFirstDate, 14 * i),
                'dd.MM.yyy'
              ),
              name: values.subjectName,
              theme: values.newTheme,
              time: format(values.practiseTime, 'hh:mm:ss'),
              week: values.practiseWeek,
              type: 'Практика'
            }
          ];
        }
        for (let i = 0; i < values.laborotoryCount; ++i) {
          subjects = [
            ...subjects, {
              day: Days[values.laborotoryFirstDate.getDay()],
              date: format(
                values.laborotoryWeek === 'Еженедельно' ?
                  addDays(values.laborotoryFirstDate, 7 * i) :
                  addDays(values.laborotoryFirstDate, 14 * i),
                'dd.MM.yyy'
              ),
              name: values.subjectName,
              theme: values.newTheme,
              time: format(values.laborotoryTime, 'hh:mm:ss'),
              week: values.laborotoryWeek,
              type: 'Лабораторная работа'
            }
          ];
        }
        setNewSubjects(subjects);
        await dispatch(createSubjects(subjects));
        setNewSubjects([]);
        close();
      }}
    >
      {(props) => (
        <Form onSubmit={props.handleSubmit}>
          <Grid
            container
            spacing={1}
            direction='column'
            sx={{minWidth: '25rem'}}
          >
            <Grid container sx={{paddingTop: '0 !important'}}>
              <TextField
                id="subjectName"
                type="text"
                variant="outlined"
                fullWidth
                onBlur={props.handleBlur}
                value={props.values.subjectName}
                onChange={props.handleChange}
                onFocus={(e) => e.target.select()}
                error={!!props.errors.subjectName}
                label="Введите название предмета..."
              />
              <FormControlLabel
                sx={{marginLeft: 'auto'}}
                control={<Checkbox id='themes'/>}
                label="Использовать темы предметов"
                value={props.values.themes}
                onChange={props.handleChange}
              />
            </Grid>
            {props.values.themes && <Grid
                item
                sx={{padding: '0 !important', marginTop: '.3rem'}}
            >
                <TextField
                    id="newTheme"
                    type="text"
                    variant="outlined"
                    fullWidth
                    onBlur={props.handleBlur}
                    value={props.values.newTheme}
                    onChange={props.handleChange}
                    onFocus={(e) => e.target.select()}
                    error={!!props.errors.newTheme}
                    label="Введите новую тему..."
                />
            </Grid>}
            {props.values.themes && props.values.themesList.length > 0 &&
            <Grid
                item
                xs={12}
                sx={{
                  padding: '0 !important',
                  marginTop: '.5rem',
                  maxHeight: '5rem',
                  overflowY: 'auto'
                }}
            >
                <List disablePadding>
                  {props.values.themesList.map(theme => (
                    <ListItem disablePadding key={theme}>
                      <ListItemText primary={theme}/>
                    </ListItem>
                  ))}
                </List>
            </Grid>}
            <Grid container spacing={2} sx={{marginTop: '.3rem'}}>
              <Grid item xs={4}>
                <CreateSubjectPairLecture {...props}/>
              </Grid>
              <Grid item xs={4}>
                <CreateSubjectPairPractise {...props}/>
              </Grid>
              <Grid item xs={4}>
                <CreateSubjectPairLaborotory {...props}/>
              </Grid>
              <ButtonGroup
                variant='outlined'
                sx={{marginLeft: 'auto', marginTop: '.3rem'}}
              >
                {props.values.themes && <Button
                    onClick={() => {
                      props.setValues({
                        ...initialSubjectTypeState,
                        themesList: [props.values.newTheme, ...props.values.themesList]
                      });
                    }}
                >Добавить тему</Button>}
                <Button
                  type='submit'
                >Добавить предмет</Button>
              </ButtonGroup>
            </Grid>
          </Grid>
        </Form>
      )}
    </Formik>
  );
};
