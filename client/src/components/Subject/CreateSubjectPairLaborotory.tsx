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

export const CreateSubjectPairLaborotory: React.FC<FormikProps<typeof initialSubjectTypeState>> = ({
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
            htmlFor="laborotoryCount"
          >Кол-во лаб</InputLabel>
          <Input
            id="laborotoryCount"
            type='number'
            value={values.laborotoryCount}
            onChange={handleChange}
            onBlur={handleBlur}
            onFocus={(e) => e.target.select()}
            error={!!errors.laborotoryCount}
            inputProps={{min: 0}}
          />
        </FormControl>
      </Grid>
      <Grid item>
        <FormControl fullWidth>
          <InputLabel id="laborotoryWeek-label">Неделя</InputLabel>
          <Select
            id="laborotoryWeek"
            labelId="laborotoryWeek-label"
            value={values.laborotoryWeek}
            label="Неделя"
            error={!!errors.laborotoryWeek}
            onChange={(e) => setFieldValue('laborotoryWeek', e.target.value)}
            onBlur={handleBlur}
          >
            <MenuItem value={'10'}>Четная</MenuItem>
            <MenuItem value={'20'}>Нечетная</MenuItem>
            <MenuItem value={'30'}>Еженедельно</MenuItem>
            <MenuItem value={'40'}>По датам</MenuItem>
          </Select>
        </FormControl>
      </Grid>
      {values.laborotoryWeek !== '' && values.laborotoryWeek !== '40' &&
      <Grid item>
          <FormControl fullWidth>
              <InputLabel id="laborotoryDay-label">День</InputLabel>
              <Select
                  id="laborotoryDay"
                  labelId="laborotoryDay-label"
                  value={values.laborotoryDay}
                  label="День"
                  error={!!errors.laborotoryDay}
                  onChange={(e) => setFieldValue('laborotoryDay', e.target.value)}
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
      {values.laborotoryWeek !== '' && values.laborotoryWeek !== '40' &&
      <Grid item>
          <LocalizationProvider
              dateAdapter={AdapterDateFns}
              locale={ru}
          >
              <TimePicker
                  label='Время проведения'
                  value={values.laborotoryTime}
                  onChange={(newValue) => setFieldValue('laborotoryTime', newValue)}
                  renderInput={(params) =>
                    <TextField
                      id='laborotoryTime'
                      error={!!errors.laborotoryTime}
                      {...params}
                    />}
              />
          </LocalizationProvider>
      </Grid>}
      {values.laborotoryWeek === '40' &&
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
