import {ThunkAction} from "redux-thunk/es/types";
import {defaultActions, DefaultActionType} from "./index";
import {API} from "../../api";
import {SignInType} from "../../model/User/SignInModel";
import {ServerErrorType} from "../../model/ServerError";

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
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(defaultActions.setLoadingInitial(false));
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
      }
    } catch (e) {
      dispatch(defaultActions.setError('Не правильный логин или пароль'));
    } finally {
      dispatch(defaultActions.setLoading(false));
    }
  }
};
