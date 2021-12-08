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
            <MenuItem value={'Каждые 2 недели'}>Через неделю</MenuItem>
            <MenuItem value={'Еженедельно'}>Еженедельно</MenuItem>
            <MenuItem value={'По определенным данным'}>По датам</MenuItem>
          </Select>
        </FormControl>
      </Grid>
      {values.laborotoryWeek !== '' &&
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
                      {...params}
                      error={!!errors.laborotoryTime}
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
                      {...params}
                      error={!!errors.laborotoryFirstDate}
                    />}
              />
          </LocalizationProvider>
      </Grid>}
      {values.laborotoryWeek === 'По определенным данным' &&
      <Grid item>
          <MultipleDatePicker
              dates={values.laborotoryDates}
              setDates={(dates: Date[]) => setFieldValue('laborotoryDates', dates)}
              error={!!errors.laborotoryDates}
          />
      </Grid>}
    </Grid>
  );
};
