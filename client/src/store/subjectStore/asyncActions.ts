import {ThunkAction} from "redux-thunk/es/types";
import {API} from "../../api";
import {ServerErrorType} from "../../model/ServerError";
import {subjectActions, SubjectActionType} from "./index";

type AsyncThunkType = ThunkAction<Promise<void>,
  null,
  unknown,
  SubjectActionType>;

export const getSubjectsInfos = () : AsyncThunkType => {
  return async dispatch => {
    dispatch(subjectActions.setLoadingInitial(true));
    try {
      const response = await API.subject.subjectsInfos();

      if (response) {
        dispatch(subjectActions.setSubjectsInfos(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(subjectActions.setLoadingInitial(false));
    }
  }
};

export const getSubjects = (name: string) : AsyncThunkType => {
  return async dispatch => {
    dispatch(subjectActions.setLoadingInitial(true));
    try {
      const response = await API.subject.subjects(name);

      if (response) {
        dispatch(subjectActions.setSubjects(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(subjectActions.setLoadingInitial(false));
    }
  }
};

export const getAllSubjects = () : AsyncThunkType => {
  return async dispatch => {
    dispatch(subjectActions.setLoading(true));
    try {
      const response = await API.subject.allSubjects();

      if (response) {
        dispatch(subjectActions.setSubjects(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(subjectActions.setLoading(false));
    }
  }
};
