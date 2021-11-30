import {
  Button,
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  Select, TextField
} from "@mui/material";
import {SubjectType} from "../../model/Subject/Subject";
import {Form, Formik} from "formik";
import {useDispatch} from "react-redux";
import {updateSubject} from "../../store/subjectStore/asyncActions";
import React from "react";
import {DatePicker, LocalizationProvider, TimePicker} from "@mui/lab";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import {ru} from "date-fns/locale";
import {format, parse} from "date-fns";

type PropsType = {
  subject: SubjectType;
};

export const UpdateSubject: React.FC<PropsType> = ({
                                                     subject
                                                   }) => {
  const dispatch = useDispatch();

  return (
    <Formik
      initialValues={{
        ...subject,
        time: parse(subject.time, 'hh:mm:ss', new Date()),
        date: parse(subject.date, 'dd.MM.yyyy', new Date())
      }}
      onSubmit={async (values) => {
        await dispatch(updateSubject(
          subject.id,
          {
            ...values,
            time: format(values.time, 'hh:mm:ss'),
            date: format(values.date, 'dd.MM.yyyy')
          }
        ));
      }}
    >
      {({
          values,
          submitForm,
          errors,
          setFieldValue,
          handleBlur,
          handleChange
        }) => (
        <Form onSubmit={submitForm}>
          <Grid
            container
            sx={{minWidth: '35rem'}}
            spacing={1}
          >
            <Grid item xs={4}>
              <LocalizationProvider
                dateAdapter={AdapterDateFns}
                locale={ru}
              >
                <TimePicker
                  label='Время проведения'
                  value={values.time}
                  onChange={(newValue) => setFieldValue('time', newValue)}
                  renderInput={(params) =>
                    <TextField
                      id='updateSubjectTime'
                      error={!!errors.time}
                      {...params}
                    />}
                />
              </LocalizationProvider>
            </Grid>
            <Grid item xs={4}>
              <TextField
                id="updateSubjectTheme"
                type="text"
                variant="outlined"
                fullWidth
                onBlur={handleBlur}
                value={values.theme}
                onChange={handleChange}
                onFocus={(e) => e.target.select()}
                error={!!errors.theme}
                label="Тема"
              />
            </Grid>
            <Grid item xs={4}>
              <LocalizationProvider
                dateAdapter={AdapterDateFns}
                locale={ru}
              >
                <DatePicker
                  mask='__.__.____'
                  label='Дата проведения'
                  value={values.date}
                  onChange={(newValue) => setFieldValue('date', newValue)}
                  renderInput={(params) =>
                    <TextField
                      id='updateSubjectDate'
                      error={!!errors.date}
                      {...params}
                    />}
                />
              </LocalizationProvider>
            </Grid>
            <Grid item xs={6}>
              <FormControl fullWidth>
                <InputLabel id="updateSubjectDay-label">День</InputLabel>
                <Select
                  id="updateSubjectDay"
                  labelId="updateSubjectDay-label"
                  value={values.day}
                  label="День"
                  error={!!errors.day}
                  onChange={(e) => setFieldValue('day', e.target.value)}
                  onBlur={handleBlur}
                >
                  <MenuItem value={'Понедельник'}>Понедельник</MenuItem>
                  <MenuItem value={'Вторник'}>Вторник</MenuItem>
                  <MenuItem value={'Среда'}>Среда</MenuItem>
                  <MenuItem value={'Четверг'}>Четверг</MenuItem>
                  <MenuItem value={'Пятница'}>Пятница</MenuItem>
                  <MenuItem value={'Суббота'}>Суббота</MenuItem>
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={6}>
              <FormControl fullWidth>
                <InputLabel id="updateSubjectType-label">Тип</InputLabel>
                <Select
                  id="updateSubjectType"
                  labelId="updateSubjectType-label"
                  value={values.type}
                  label="Тип"
                  error={!!errors.type}
                  onChange={(e) => setFieldValue('type', e.target.value)}
                  onBlur={handleBlur}
                >
                  <MenuItem value={'Лекция'}>Лекция</MenuItem>
                  <MenuItem value={'Практика'}>Практика</MenuItem>
                  <MenuItem
                    value={'Лабороторная работа'}
                  >Лабороторная работа</MenuItem>
                </Select>
              </FormControl>
            </Grid>
            <Button
              type='submit'
              variant='outlined'
              sx={{marginLeft: 'auto', marginTop: '.5rem'}}
            >Обновить</Button>
          </Grid>
        </Form>
      )}
    </Formik>
  );
};
