import {ThunkAction} from "redux-thunk/es/types";
import {defaultActions, DefaultActionType} from "./index";
import {API} from "../../api";
import {SignInType} from "../../model/User/SignInModel";
import {ServerErrorType} from "../../model/ServerError";
import {SignUpType} from "../../model/User/SugnUpModel";

type AsyncThunkType = ThunkAction<Promise<void>,
  null,
  unknown,
  DefaultActionType>;

export const isAuthAsync = (): AsyncThunkType => {
  return async dispatch => {
    try {
      const response = await API.auth.isAuth();

      if (response) {
        dispatch(defaultActions.setIsAuth(true));
        dispatch(defaultActions.setRoles(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(defaultActions.setLoadingInitial(false));
    }
  }
};

export const logoutAsync = (): AsyncThunkType => {
  return async dispatch => {
    dispatch(defaultActions.setLoading(true));
    try {
      const response = await API.auth.logout();

      if (response) {
        dispatch(defaultActions.setIsAuth(false));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(defaultActions.setLoading(false));
    }
  }
};

export const signInAsync = (
  payload: SignInType
): AsyncThunkType => {
  return async dispatch => {
    dispatch(defaultActions.setLoading(true));
    try {
      const response = await API.auth.signIn(payload);

      if (response) {
        dispatch(defaultActions.setIsAuth(true));
        dispatch(defaultActions.setRoles(response));
      }
    } catch (e) {
      dispatch(defaultActions.setError('Не правильный логин или пароль'));
    } finally {
      dispatch(defaultActions.setLoading(false));
    }
  }
};

export const signUpAsync = (
  payload: SignUpType
): AsyncThunkType => {
  return async dispatch => {
    dispatch(defaultActions.setLoading(true));
    try {
      const response = await API.auth.signUp(payload);

      if (response) {
        dispatch(defaultActions.setIsAuth(true));
      }
    } catch (e) {
      dispatch(defaultActions.setError('Данный пользователь существует'));
    } finally {
      dispatch(defaultActions.setLoading(false));
    }
  }
};
