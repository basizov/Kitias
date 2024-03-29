import {
  Button, ButtonGroup, Checkbox,
  FormControl, FormControlLabel,
  Grid,
  InputLabel,
  MenuItem,
  Select, TextField, useMediaQuery
} from "@mui/material";
import {SubjectType} from "../../model/Subject/Subject";
import {Form, Formik} from "formik";
import {useDispatch} from "react-redux";
import {
  deleteSubject,
  updateSubject
} from "../../store/subjectStore/asyncActions";
import React, {useMemo} from "react";
import {DatePicker, LocalizationProvider, TimePicker} from "@mui/lab";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import {ru} from "date-fns/locale";
import {format, parse} from "date-fns";
import {SchemaOptions} from "yup/es/schema";
import {boolean, object, string} from "yup/es";

type PropsType = {
  subject: SubjectType;
  close: () => void;
};

export const UpdateSubject: React.FC<PropsType> = ({
                                                     subject,
                                                     close
                                                   }) => {
  const dispatch = useDispatch();
  const isTablet = useMediaQuery('(min-width: 620px)');
  const validationSchema: SchemaOptions<SubjectType> = useMemo(() => {
    return object({
      type: string().required(),
      time: string().required(),
      date: string().required(),
      theme: string(),
      isGiveScore: boolean()
    });
  }, []);

  return (
    <Formik
      initialValues={{
        ...subject,
        time: parse(subject.time, 'H:mm:ss', new Date()),
        date: parse(subject.date, 'dd.MM.yyyy', new Date())
      }}
      validationSchema={validationSchema}
      onSubmit={async (values) => {
        await dispatch(updateSubject(
          subject.id,
          {
            ...values,
            time: format(values.time, 'H:mm:ss'),
            date: format(values.date, 'dd.MM.yyyy')
          }
        ));
        close();
      }}
    >
      {({
          values,
          handleSubmit,
          errors,
          setFieldValue,
          handleBlur,
          handleChange
        }) => (
        <Form onSubmit={handleSubmit}>
          <Grid
            container
            sx={{minWidth: isTablet ? '35rem' : '17rem'}}
            spacing={1}
          >
            <Grid item xs={12} sm={6}>
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
                      fullWidth
                    />}
                />
              </LocalizationProvider>
            </Grid>
            <Grid item xs={12} sm={6}>
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
                      fullWidth
                    />}
                />
              </LocalizationProvider>
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                id="theme"
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
            <Grid item xs={12} sm={6}>
              <FormControl fullWidth>
                <InputLabel id="updateSubjectType-label">Тип</InputLabel>
                <Select
                  id="type"
                  labelId="updateSubjectType-label"
                  value={values.type}
                  label="Тип"
                  error={!!errors.type}
                  onChange={(e) => setFieldValue('type', e.target.value)}
                >
                  <MenuItem value={'Лекция'}>Лекция</MenuItem>
                  <MenuItem value={'Практика'}>Практика</MenuItem>
                  <MenuItem
                    value={'Лабораторная работа'}
                  >Лабороторная работа</MenuItem>
                </Select>
              </FormControl>
            </Grid>
            <FormControlLabel
              sx={{marginLeft: 'auto'}}
              control={<Checkbox
                id='isGiveScore'
                checked={values.isGiveScore}
                onChange={handleChange}
              />}
              label="Возможность проставлять баллы"
            />
            <Grid item xs={12}></Grid>
            <ButtonGroup
              variant='outlined'
              size='small'
              sx={{marginLeft: 'auto', marginTop: '-.5rem'}}
            >
              <Button
                type='submit'
              >Обновить</Button>
              <Button
                color='error'
                onClick={async () => {
                  await dispatch(deleteSubject(subject.id));
                  close();
                }}
              >Удалить</Button>
            </ButtonGroup>
          </Grid>
        </Form>
      )}
    </Formik>
  );
};
