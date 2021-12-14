import React, {useMemo} from 'react';
import {SchemaOptions} from "yup/es/schema";
import {array, date, number, object, string} from "yup/es";
import {addMonths, addYears, format} from "date-fns";
import {Form, Formik} from "formik";
import {useDispatch} from "react-redux";
import {CreateGroupStudentsType} from "../../model/Group/CreateGroup";
import {createGroup} from "../../store/groupStore/asyncActions";
import {
  Button,
  ButtonGroup,
  FormControl,
  Grid,
  InputLabel, MenuItem,
  Select,
  TextField, useMediaQuery
} from "@mui/material";
import {DatePicker, LocalizationProvider} from "@mui/lab";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import {ru} from "date-fns/locale";
import {EditStudents} from "./EditStudents";

const initialCreateGroupState: CreateGroupStudentsType = {
  course: 1 as number,
  number: '',
  educationType: 'Очное обучение',
  speciality: 'Программирование в компьютерных системах',
  receiptDate: new Date(),
  issueDate: addYears(new Date(), 4),
  newStudent: '',
  students: [] as string[]
};

type PropsType = {
  close: () => void;
};

export const CreateGroup: React.FC<PropsType> = ({close}) => {
  const dispatch = useDispatch();
  const isMobile = useMediaQuery('(min-width: 450px)');
  const validationSchema: SchemaOptions<CreateGroupStudentsType> = useMemo(() => {
    return object({
      course: number().required().min(1).max(4),
      number: string().required(),
      educationType: string().required(),
      speciality: string().required(),
      newStudent: string(),
      receiptDate: date().required(),
      issueDate: date().required(),
      students: array().of(string())
    });
  }, []);

  return (
    <Formik
      initialValues={initialCreateGroupState}
      validationSchema={validationSchema}
      onSubmit={async (values) => {
        await dispatch(createGroup(values));
        close();
      }}
    >
      {({
          handleSubmit,
          handleBlur,
          handleChange,
          values,
          errors,
          setFieldValue
        }) => (
        <Form onSubmit={handleSubmit}>
          <Grid
            container
            spacing={1}
            sx={{minWidth: `${isMobile ? '25rem' : '17rem'}`}}
          >
            <Grid item xs={12} sm={6}>
              <TextField
                id="number"
                type="text"
                variant="outlined"
                fullWidth
                onBlur={handleBlur}
                value={values.number}
                onChange={handleChange}
                onFocus={(e) => e.target.select()}
                error={!!errors.number}
                label="Номер группы"
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                id="course"
                type="number"
                variant="outlined"
                fullWidth
                onBlur={handleBlur}
                value={values.course}
                onChange={handleChange}
                onFocus={(e) => e.target.select()}
                error={!!errors.course}
                InputProps={{inputProps: {min: 1, max: 4}}}
                label="Курс"
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <FormControl fullWidth>
                <InputLabel id="educationType-label">Тип обучения</InputLabel>
                <Select
                  id="educationType"
                  labelId="educationType-label"
                  value={values.educationType}
                  label="Тип обучения"
                  error={!!errors.educationType}
                  onChange={(e) => setFieldValue('educationType', e.target.value)}
                >
                  <MenuItem
                    value={'Очное обучение'}
                  >Очное обучение</MenuItem>
                  <MenuItem
                    value={'Заочное обучение'}
                  >Заочное обучение</MenuItem>
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12} sm={6}>
              <FormControl fullWidth>
                <InputLabel id="speciality-label">Специальность</InputLabel>
                <Select
                  id="speciality"
                  labelId="speciality-label"
                  value={values.speciality}
                  label="Специальность"
                  error={!!errors.speciality}
                  onChange={(e) => setFieldValue('speciality', e.target.value)}
                >
                  <MenuItem
                    value={'Программирование в компьютерных системах'}
                  >Программирование в компьютерных системах</MenuItem>
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12} sm={6}>
              <LocalizationProvider
                dateAdapter={AdapterDateFns}
                locale={ru}
              >
                <DatePicker
                  mask='__.__.____'
                  label='Дата поступления'
                  value={values.receiptDate}
                  onChange={(newValue) => {
                    if (newValue) {
                      setFieldValue('receiptDate', newValue);
                      setFieldValue(
                        'issueDate',
                        addYears(addMonths(newValue, 10), 3)
                      );
                    }
                  }}
                  renderInput={(params) =>
                    <TextField
                      id='receiptDate'
                      {...params}
                      fullWidth
                      error={!!errors.receiptDate}
                    />}
                />
              </LocalizationProvider>
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                id="issueDate"
                type="text"
                variant="outlined"
                fullWidth
                disabled
                onBlur={handleBlur}
                value={format(values.issueDate, 'dd.MM.yyyy')}
                onChange={handleChange}
                onFocus={(e) => e.target.select()}
                error={!!errors.issueDate}
                label="Дата выпуска"
              />
            </Grid>
            <Grid item xs={12}>
              <EditStudents
                id='newStudent'
                newStudent={values.newStudent}
                setNewStudent={(value) => setFieldValue('newStudent', value)}
                students={values.students.map(s => ({
                  id: '',
                  fullName: s
                }))}
                setStudents={(newStudents) => setFieldValue(
                  'students',
                  newStudents.map(s => s.fullName)
                )}
              />
            </Grid>
            <ButtonGroup
              size='small'
              sx={{marginLeft: 'auto', marginTop: '.3rem'}}
            >
              <Button
                type='submit'
              >Создать</Button>
            </ButtonGroup>
          </Grid>
        </Form>
      )}
    </Formik>
  );
};
