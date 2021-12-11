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
import {MultipleDatePicker} from "../MultipleDatePicker";

export const CreateSubjectPairPractise: React.FC<FormikProps<typeof initialSubjectTypeState>> = ({
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
            htmlFor="practiseCount"
          >Кол-во практик</InputLabel>
          <Input
            id="practiseCount"
            type='number'
            value={values.practiseCount}
            onChange={handleChange}
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
          >
            <MenuItem value={'Каждые 2 недели'}>Через неделю</MenuItem>
            <MenuItem value={'Еженедельно'}>Еженедельно</MenuItem>
            <MenuItem value={'По определенным данным'}>По датам</MenuItem>
          </Select>
        </FormControl>
      </Grid>
      {values.practiseWeek !== '' &&
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
                      {...params}
                      fullWidth
                      error={!!errors.practiseTime}
                    />}
              />
          </LocalizationProvider>
      </Grid>}
      {values.practiseWeek !== '' && values.practiseWeek !== 'По определенным данным' &&
      <Grid item>
          <LocalizationProvider
              dateAdapter={AdapterDateFns}
              locale={ru}
          >
              <DatePicker
                  mask='__.__.____'
                  label='Дата первого занятия'
                  value={values.practiseFirstDate}
                  onChange={(newValue) => setFieldValue('practiseFirstDate', newValue)}
                  renderInput={(params) =>
                    <TextField
                      id='practiseFirstDate'
                      {...params}
                      fullWidth
                      error={!!errors.practiseFirstDate}
                    />}
              />
          </LocalizationProvider>
      </Grid>}
      {values.practiseWeek === 'По определенным данным' &&
      <Grid item>
          <MultipleDatePicker
              id='practiseDates'
              dates={values.practiseDates}
              setDates={(dates: Date[]) => setFieldValue('practiseDates', dates)}
              error={!!errors.practiseDates}
          />
      </Grid>}
    </Grid>
  );
};
