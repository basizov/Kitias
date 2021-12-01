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
import {DatePicker, LocalizationProvider, TimePicker} from "@mui/lab";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import {ru} from "date-fns/locale";

export const CreateSubjectPairLaborotory: React.FC<FormikProps<typeof initialSubjectTypeState>> = ({
                                                                                                   values,
                                                                                                   handleChange,
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
          >
            <MenuItem value={'Четная неделя'}>Четная</MenuItem>
            <MenuItem value={'Нечетная неделя'}>Нечетная</MenuItem>
            <MenuItem value={'Еженедельно'}>Еженедельно</MenuItem>
            <MenuItem value={'По определенным данным'}>По датам</MenuItem>
          </Select>
        </FormControl>
      </Grid>
      {values.laborotoryWeek !== '' && values.laborotoryWeek !== 'По определенным данным' &&
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
      {values.laborotoryWeek !== '' && values.laborotoryWeek !== 'По определенным данным' &&
      <Grid item>
          <LocalizationProvider
              dateAdapter={AdapterDateFns}
              locale={ru}
          >
              <DatePicker
                  mask='__.__.____'
                  label='Дата первого занятия'
                  value={values.laborotoryFirstDate}
                  onChange={(newValue) => setFieldValue('laborotoryFirstDate', newValue)}
                  renderInput={(params) =>
                    <TextField
                      id='laborotoryFirstDate'
                      error={!!errors.laborotoryFirstDate}
                      {...params}
                    />}
              />
          </LocalizationProvider>
      </Grid>}
      {values.laborotoryWeek === 'По определенным данным' &&
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
