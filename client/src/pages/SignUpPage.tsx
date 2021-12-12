import React, {useMemo} from 'react';
import {useDispatch} from "react-redux";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {SchemaOptions} from "yup/es/schema";
import {object, string} from "yup/es";
import {SignUpType} from "../model/User/SugnUpModel";
import {Form, Formik} from "formik";
import {signUpAsync} from "../store/defaultStore/asyncActions";
import {
  Button,
  ButtonGroup,
  CircularProgress,
  Grid,
  TextField,
  Typography
} from "@mui/material";
import {useNavigate} from "react-router";
import {AuthPaper, StyledGrid} from "./AuthPage";
import {defaultActions} from "../store/defaultStore";

export const SignUpPage: React.FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const {error, loading} = useTypedSelector(s => s.common);
  const validationSchema: SchemaOptions<SignUpType> = useMemo(() => {
    return object({
      userName: string().required(),
      password: string().required(),
      email: string().required(),
      name: string().required(),
      surname: string().required(),
      patronymic: string().required()
    });
  }, []);

  return (
    <AuthPaper>
      <Formik
        initialValues={{
          userName: '',
          password: '',
          email: '',
          name: '',
          surname: '',
          patronymic: ''
        } as SignUpType}
        validationSchema={validationSchema}
        onSubmit={async (values) => {
          await dispatch(signUpAsync(values));
        }}
      >
        {({
            handleSubmit,
            handleBlur,
            values,
            handleChange,
            errors,
            isValid,
            dirty,
            resetForm
          }) => (
          <Form onSubmit={handleSubmit}>
            <StyledGrid
              container
              spacing={1}
            >
              <Grid item xs={12} sm={6}>
                <TextField
                  id="email"
                  type="text"
                  variant="filled"
                  fullWidth
                  onBlur={handleBlur}
                  value={values.email}
                  onChange={handleChange}
                  onFocus={(e) => e.target.select()}
                  error={!!errors.email}
                  label="E-mail"
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  id="userName"
                  type="text"
                  variant="filled"
                  fullWidth
                  onBlur={handleBlur}
                  value={values.userName}
                  onChange={handleChange}
                  onFocus={(e) => e.target.select()}
                  error={!!errors.userName}
                  label="Логин"
                />
              </Grid>
              <Grid item xs={6} sm={4}>
                <TextField
                  id="name"
                  type="text"
                  variant="filled"
                  fullWidth
                  onBlur={handleBlur}
                  value={values.name}
                  onChange={handleChange}
                  onFocus={(e) => e.target.select()}
                  error={!!errors.name}
                  label="Имя"
                />
              </Grid>
              <Grid item xs={6} sm={4}>
                <TextField
                  id="surname"
                  type="text"
                  variant="filled"
                  fullWidth
                  onBlur={handleBlur}
                  value={values.surname}
                  onChange={handleChange}
                  onFocus={(e) => e.target.select()}
                  error={!!errors.surname}
                  label="Фамилия"
                />
              </Grid>
              <Grid item xs={12} sm={4}>
                <TextField
                  id="patronymic"
                  type="text"
                  variant="filled"
                  fullWidth
                  onBlur={handleBlur}
                  value={values.patronymic}
                  onChange={handleChange}
                  onFocus={(e) => e.target.select()}
                  error={!!errors.patronymic}
                  label="Отчество"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  id="password"
                  type="password"
                  variant="filled"
                  fullWidth
                  onBlur={handleBlur}
                  value={values.password}
                  onChange={handleChange}
                  onFocus={(e) => e.target.select()}
                  error={!!errors.password}
                  label="Пароль"
                />
              </Grid>
              <Grid item xs={12}>
                <Grid
                  container
                  justifyContent="space-between"
                  alignItems="center"
                  spacing={1}
                >
                  <Grid item>
                    <Typography
                      variant="caption"
                      component="div"
                      color='error'
                    >{error || ''}</Typography>
                  </Grid>
                  <Grid item>
                    <ButtonGroup variant='outlined'>
                      <Button
                        color='secondary'
                        sx={{width: '7rem'}}
                        onClick={() => resetForm()}
                      >Сбросить</Button>
                      <Button
                        type='submit'
                        sx={{width: '7rem'}}
                        disabled={!isValid && !dirty}
                      >{loading ?
                        <CircularProgress
                          color="inherit"
                          size={14}
                        /> : 'Создать'}</Button>
                    </ButtonGroup>
                  </Grid>
                </Grid>
              </Grid>
              <Button
                color='info'
                size='small'
                sx={{marginLeft: 'auto'}}
                onClick={() => {
                  dispatch(defaultActions.setError(''))
                  navigate('/login');
                }}
              >У вас уже есть аккаунт?</Button>
            </StyledGrid>
          </Form>
        )}
      </Formik>
    </AuthPaper>
  );
};
