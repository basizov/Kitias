import React from 'react';
import {FormikProps} from "formik";
import {initialSubjectTypeState} from "./CreateSubject";
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

export const CreateSubjectPairPractise: React.FC<FormikProps<typeof initialSubjectTypeState>> = ({
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
            htmlFor="practiseCount"
          >Кол-во практик</InputLabel>
          <Input
            id="practiseCount"
            type='number'
            value={values.practiseCount}
            onChange={handleChange}
            onBlur={handleBlur}
            onFocus={(e) => e.target.select()}
            error={!!errors.practiseCount}
            inputProps={{min: 0}}
          />
        </FormControl>
      </Grid>
      <Grid item>
        <FormControl fullWidth>
          <InputLabel id="practiseWeek-label">Неделя</InputLabel>
          <Select
            id="practiseWeek"
            labelId="practiseWeek-label"
            value={values.practiseWeek}
            label="Неделя"
            error={!!errors.practiseWeek}
            onChange={(e) => setFieldValue('practiseWeek', e.target.value)}
            onBlur={handleBlur}
          >
            <MenuItem value={'10'}>Четная</MenuItem>
            <MenuItem value={'20'}>Нечетная</MenuItem>
            <MenuItem value={'30'}>Еженедельно</MenuItem>
            <MenuItem value={'40'}>По датам</MenuItem>
          </Select>
        </FormControl>
      </Grid>
      {values.practiseWeek !== '' && values.practiseWeek !== '40' &&
      <Grid item>
          <FormControl fullWidth>
              <InputLabel id="practiseDay-label">День</InputLabel>
              <Select
                  id="practiseDay"
                  labelId="practiseDay-label"
                  value={values.practiseDay}
                  label="День"
                  error={!!errors.practiseDay}
                  onChange={(e) => setFieldValue('practiseDay', e.target.value)}
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
      {values.practiseWeek !== '' && values.practiseWeek !== '40' &&
      <Grid item>
          <LocalizationProvider
              dateAdapter={AdapterDateFns}
              locale={ru}
          >
              <TimePicker
                  label='Время проведения'
                  value={values.practiseTime}
                  onChange={(newValue) => setFieldValue('practiseTime', newValue)}
                  renderInput={(params) =>
                    <TextField
                      id='practiseTime'
                      error={!!errors.practiseTime}
                      {...params}
                    />}
              />
          </LocalizationProvider>
      </Grid>}
      {values.practiseWeek === '40' &&
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
