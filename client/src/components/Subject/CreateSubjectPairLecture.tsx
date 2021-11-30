import React from 'react';
import {
  FormControl,
  Grid,
  Input,
  InputLabel,
  MenuItem,
  Select, TextField
} from "@mui/material";
import {LocalizationProvider, TimePicker} from "@mui/lab";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import {ru} from "date-fns/locale";
import {FormikProps} from "formik";
import {initialSubjectTypeState} from "./CreateSubject";

export const CreateSubjectPairLecture: React.FC<FormikProps<typeof initialSubjectTypeState>> = ({
                                                                                           values,
                                                                                           handleChange,
                                                                                           handleBlur,
                                                                                           errors,
                                                                                           setFieldValue
                                                                                         }) => {
  return (
    <Grid container direction='column' spacing={2}>
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
            onBlur={handleBlur}
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
            onBlur={handleBlur}
          >
            <MenuItem value={'10'}>Четная</MenuItem>
            <MenuItem value={'20'}>Нечетная</MenuItem>
            <MenuItem value={'30'}>Еженедельно</MenuItem>
            <MenuItem value={'40'}>По датам</MenuItem>
          </Select>
        </FormControl>
      </Grid>
      {values.lectureWeek !== '' && values.lectureWeek !== '40' &&
      <Grid item>
          <FormControl fullWidth>
              <InputLabel id="lectureDay-label">День</InputLabel>
              <Select
                  id="lectureDay"
                  labelId="lectureDay-label"
                  value={values.lectureDay}
                  label="День"
                  error={!!errors.lectureDay}
                  onChange={(e) => setFieldValue('lectureDay', e.target.value)}
                  onBlur={handleBlur}
              >
                  <MenuItem value={'10'}>Понедельник</MenuItem>
                  <MenuItem value={'20'}>Вторник</MenuItem>
                  <MenuItem value={'30'}>Среда</MenuItem>
                  <MenuItem value={'40'}>Четверг</MenuItem>
                  <MenuItem value={'50'}>Пятница</MenuItem>
                  <MenuItem value={'60'}>Суббота</MenuItem>
              </Select>
          </FormControl>
      </Grid>}
      {values.lectureWeek !== '' && values.lectureWeek !== '40' &&
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
                      error={!!errors.lectureTime}
                      {...params}
                    />}
              />
          </LocalizationProvider>
      </Grid>}
      {values.lectureWeek === '40' &&
      <Grid item>
        {/*<DatePicker*/}
        {/*    id='lectureDates'*/}
        {/*    multiple*/}
        {/*    value={values.lectureDates}*/}
        {/*    onChange={handleChange}*/}
        {/*/>*/}
      </Grid>}
    </Grid>
  );
};
