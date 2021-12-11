import React, {useMemo, useState} from 'react';
import {
  Button, ButtonGroup,
  Checkbox,
  FormControlLabel,
  Grid, List, ListItem, ListItemText,
  TextField, useMediaQuery
} from "@mui/material";
import {Form, Formik} from "formik";
import {CreateSubjectPairLecture} from "./CreateSubjectPairLecture";
import {CreateSubjectPairPractise} from "./CreateSubjectPairPractise";
import {CreateSubjectPairLaborotory} from "./CreateSubjectPairLaborotory";
import {CreateSubjectType} from "../../model/Subject/CreateSubjectModel";
import {createSubjects} from "../../store/subjectStore/asyncActions";
import {addDays, format} from "date-fns";
import {useDispatch} from "react-redux";
import {object, string, number, date, array, ref} from "yup/es";
import {SchemaOptions} from "yup/es/schema";

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
  lectureDates: [new Date()] as Date[],
  practiseCount: 0 as number,
  practiseWeek: '' as string,
  practiseTime: new Date(),
  practiseDates: [new Date()] as Date[],
  laborotoryCount: 0 as number,
  laborotoryWeek: '' as string,
  laborotoryTime: new Date(),
  laborotoryDates: [new Date()] as Date[]
} as const;

type PropsType = {
  close: () => void;
};

export const CreateSubject: React.FC<PropsType> = ({close}) => {
  const dispatch = useDispatch();
  const [newSubjects, setNewSubjects] = useState<CreateSubjectType[]>([]);
  const isMobile = useMediaQuery('(min-width: 450px)');
  const Days = useMemo(() => [
    'Понедельник',
    'Вторник',
    'Среда',
    'Четверг',
    'Пятница',
    'Суббота',
    'Воскресенье'
  ], []);

  const validationSchema: SchemaOptions<typeof initialSubjectTypeState> = useMemo(() => {
    return object({
      subjectName: string().required(),
      lectureCount: number().min(0).max(100),
      lectureWeek: string().when('lectureCount', {
        is: (lectureCount: number) => lectureCount > 0,
        then: string().required()
      }),
      lectureFirstDate: date().when('lectureWeek', {
        is: (lectureWeek: string) => lectureWeek === 'По определенным данным',
        then: date().required()
      }),
      practiseFirstDate: date().when('practiseWeek', {
        is: (practiseWeek: string) => practiseWeek === 'По определенным данным',
        then: date().required()
      }),
      laborotoryFirstDate: date().when('laborotoryWeek', {
        is: (laborotoryWeek: string) => laborotoryWeek === 'По определенным данным',
        then: date().required()
      }),
      lectureTime: date().when('lectureCount', {
        is: (lectureCount: number) => lectureCount > 0,
        then: date().required()
      }),
      lectureDates: array().when('lectureWeek', {
        is: (lectureWeek: string) => lectureWeek === 'По определенным данным',
        then: array().of(date())
          .min(ref('lectureCount'))
          .max(ref('lectureCount'))
      }),
      practiseCount: number().min(0).max(100),
      practiseWeek: string().when('practiseCount', {
        is: (practiseCount: number) => practiseCount > 0,
        then: string().required()
      }),
      practiseTime: date().when('practiseCount', {
        is: (practiseCount: number) => practiseCount > 0,
        then: date().required()
      }),
      practiseDates: array().when('practiseWeek', {
        is: (practiseWeek: string) => practiseWeek === 'По определенным данным',
        then: array().of(date())
          .min(ref('practiseCount'))
          .max(ref('practiseCount'))
      }),
      laborotoryCount: number().min(0).max(100),
      laborotoryWeek: string().when('laborotoryCount', {
        is: (laborotoryCount: number) => laborotoryCount > 0,
        then: string().required()
      }),
      laborotoryTime: date().when('laborotoryCount', {
        is: (laborotoryCount: number) => laborotoryCount > 0,
        then: date().required()
      }),
      laborotoryDates: array().when('laborotoryWeek', {
        is: (laborotoryWeek: string) => laborotoryWeek === 'По определенным данным',
        then: array().of(date())
          .min(ref('laborotoryCount'))
          .max(ref('laborotoryCount'))
      })
    });
  }, []);

  const setNewSubjectHandler = (
    count: number,
    firstDate: Date,
    time: Date,
    week: string,
    subjectName: string,
    subjectTypeName: string,
    theme: string,
    dates: Date[]
  ): CreateSubjectType[] => {
    let subjects = [] as CreateSubjectType[];

    for (let i = 0; i < count; ++i) {
      subjects = [
        ...subjects, {
          day: Days[firstDate.getDay()],
          date: dates.length === 0 ? format(
            week === 'Еженедельно' ?
              addDays(firstDate, 7 * i) :
              addDays(firstDate, 14 * i),
            'dd.MM.yyyy'
          ) : format(dates[i], "dd.MM.yyyy"),
          name: subjectName,
          theme: theme,
          time: format(time, 'hh:mm:ss'),
          week: week,
          type: subjectTypeName
        }
      ];
    }
    return subjects;
  };

  const getSubjectsFromValues = (
    values: typeof initialSubjectTypeState
  ): CreateSubjectType[] => {
    const lectures = setNewSubjectHandler(
      values.lectureCount,
      values.lectureFirstDate,
      values.lectureTime,
      values.lectureWeek,
      values.subjectName,
      'Лекция',
      values.newTheme,
      values.lectureDates
    );
    const practises = setNewSubjectHandler(
      values.practiseCount,
      values.practiseFirstDate,
      values.practiseTime,
      values.practiseWeek,
      values.subjectName,
      'Практика',
      values.newTheme,
      values.practiseDates
    );
    const laborotories = setNewSubjectHandler(
      values.laborotoryCount,
      values.laborotoryFirstDate,
      values.laborotoryTime,
      values.laborotoryWeek,
      values.subjectName,
      'Лабораторная работа',
      values.newTheme,
      values.laborotoryDates
    );

    return [
      ...lectures,
      ...practises,
      ...laborotories,
      ...newSubjects
    ];
  };

  return (
    <Formik
      initialValues={initialSubjectTypeState}
      validationSchema={validationSchema}
      onSubmit={async (values) => {
        const subjects = getSubjectsFromValues(values);

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
            sx={{minWidth: `${isMobile ? '25rem' : '18rem'}`}}
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
              <Grid item xs={12} sm={4}>
                <CreateSubjectPairLecture {...props}/>
              </Grid>
              <Grid item xs={12} sm={4}>
                <CreateSubjectPairPractise {...props}/>
              </Grid>
              <Grid item xs={12} sm={4}>
                <CreateSubjectPairLaborotory {...props}/>
              </Grid>
              <ButtonGroup
                variant='outlined'
                size='small'
                sx={{marginLeft: 'auto', marginTop: '.3rem'}}
              >
                {props.values.themes && <Button
                    type='button'
                    onClick={() => {
                      if (props.values.newTheme) {
                        const subjects = getSubjectsFromValues(props.values);

                        setNewSubjects(subjects);
                        props.setValues({
                          ...initialSubjectTypeState,
                          themesList: [props.values.newTheme, ...props.values.themesList]
                        });
                      }
                    }}
                >Добавить тему</Button>}
                <Button
                  type='submit'
                >Создать предмет</Button>
              </ButtonGroup>
            </Grid>
          </Grid>
        </Form>
      )}
    </Formik>
  );
};
