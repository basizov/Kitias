import React from 'react';
import {
  Button,
  ButtonGroup, CircularProgress,
  Grid,
  Paper,
  styled,
  TextField, Typography
} from "@mui/material";
import {useDispatch} from "react-redux";
import {Form, Formik} from "formik";
import {SignInType} from "../model/User/SignInModel";
import {signInAsync} from "../store/defaultStore/asyncActions";
import {SchemaOptions} from "yup/es/schema";
import {object, string} from "yup/es";
import {useTypedSelector} from "../hooks/useTypedSelector";

const AuthPaper = styled(Paper)({
  position: 'absolute',
  top: 0,
  left: 0,
  width: '100%',
  minHeight: '100vh',
  borderRadius: 0,
  zIndex: 10000,
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center'
});

export const AuthPage: React.FC = () => {
  const dispatch = useDispatch();
  const {error, loading} = useTypedSelector(s => s.common);
  const validationSchema: SchemaOptions<SignInType> = object({
    userName: string().required(),
    password: string().required()
  });

  return (
    <AuthPaper>
      <Formik
        initialValues={{
          userName: '',
          password: ''
        } as SignInType}
        validationSchema={validationSchema}
        onSubmit={async (values) => {
          await dispatch(signInAsync(values));
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
            <Grid
              container
              spacing={1}
              direction='column'
              sx={{minWidth: '30rem'}}
            >
              <Grid item sx={{paddingLeft: '0 !important'}}>
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
                  label="Введите ваш логин..."
                />
              </Grid>
              <Grid item sx={{paddingLeft: '0 !important'}}>
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
                  label="Введите ваш пароль..."
                />
              </Grid>
              <Grid
                container
                justifyContent="space-between"
                alignItems="center"
                sx={{marginTop: '.5rem'}}
              >
                <Grid item>
                  <Typography
                    variant="caption"
                    component="div"
                    color='secondary'
                    sx={{paddingLeft: '.5rem'}}
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
                      /> : 'Войти'}</Button>
                  </ButtonGroup>
                </Grid>
              </Grid>
            </Grid>
          </Form>
        )}
      </Formik>
    </AuthPaper>
  );
};
