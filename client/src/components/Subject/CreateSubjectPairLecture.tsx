import React from 'react';
import {
  FormControl,
  Grid,
  Input,
  InputLabel,
  MenuItem,
  Select, TextField
} from "@mui/material";
import {DatePicker, LocalizationProvider, TimePicker} from "@mui/lab";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import {ru} from "date-fns/locale";
import {FormikProps} from "formik";
import {initialSubjectTypeState} from "./CreateSubject";
import {MultipleDatePicker} from "../MultipleDatePicker";

export const CreateSubjectPairLecture: React.FC<FormikProps<typeof initialSubjectTypeState>> = ({
                                                                                                  values,
                                                                                                  handleChange,
                                                                                                  errors,
                                                                                                  setFieldValue
                                                                                                }) => {
  return (
    <Grid container direction='column' spacing={1}>
      <Grid item>
        <FormControl variant="standard" fullWidth>
          <InputLabel
            htmlFor="lectureCount"
          >Кол-во лекций</InputLabel>
          <Input
            id="lectureCount"
            type='number'
            value={values.lectureCount}
            onChange={handleChange}
            onFocus={(e) => e.target.select()}
            error={!!errors.lectureCount}
            inputProps={{min: 0}}
          />
        </FormControl>
      </Grid>
      <Grid item>
        <FormControl fullWidth>
          <InputLabel id="lectureWeek-label">Неделя</InputLabel>
          <Select
            id="lectureWeek"
            labelId="lectureWeek-label"
            value={values.lectureWeek}
            label="Неделя"
            error={!!errors.lectureWeek}
            onChange={(e) => setFieldValue('lectureWeek', e.target.value)}
          >
            <MenuItem value={'Каждые 2 недели'}>Через неделю</MenuItem>
            <MenuItem value={'Еженедельно'}>Еженедельно</MenuItem>
            <MenuItem value={'По определенным данным'}>По датам</MenuItem>
          </Select>
        </FormControl>
      </Grid>
      {values.lectureWeek !== '' &&
      <Grid item>
          <LocalizationProvider
              dateAdapter={AdapterDateFns}
              locale={ru}
          >
              <TimePicker
                  label='Время проведения'
                  value={values.lectureTime}
                  onChange={(newValue) => setFieldValue('lectureTime', newValue)}
                  renderInput={(params) =>
                    <TextField
                      id='lectureTime'
                      {...params}
                      fullWidth
                      error={!!errors.lectureTime}
                    />}
              />
          </LocalizationProvider>
      </Grid>}
      {values.lectureWeek !== '' && values.lectureWeek !== 'По определенным данным' &&
      <Grid item>
          <LocalizationProvider
              dateAdapter={AdapterDateFns}
              locale={ru}
          >
              <DatePicker
                  mask='__.__.____'
                  label='Дата первого занятия'
                  value={values.lectureFirstDate}
                  onChange={(newValue) => setFieldValue('lectureFirstDate', newValue)}
                  renderInput={(params) =>
                    <TextField
                      id='lectureFirstDate'
                      {...params}
                      fullWidth
                      error={!!errors.lectureFirstDate}
                    />}
              />
          </LocalizationProvider>
      </Grid>}
      {values.lectureWeek === 'По определенным данным' &&
      <Grid item>
          <MultipleDatePicker
              dates={values.lectureDates}
              id='lectureDates'
              setDates={(dates: Date[]) => setFieldValue('lectureDates', dates)}
              error={!!errors.lectureDates}
          />
      </Grid>}
    </Grid>
  );
};
