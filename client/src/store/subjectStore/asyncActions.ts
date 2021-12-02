import {ThunkAction} from "redux-thunk/es/types";
import {API} from "../../api";
import {ServerErrorType} from "../../model/ServerError";
import {subjectActions, SubjectActionType} from "./index";
import {CreateSubjectType} from "../../model/Subject/CreateSubjectModel";
import {RootState} from "../index";

type AsyncThunkType = ThunkAction<Promise<void>,
  RootState,
  unknown,
  SubjectActionType>;

export const getSubjectsInfos = (): AsyncThunkType => {
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

export const getSubjects = (name: string): AsyncThunkType => {
  return async dispatch => {
    dispatch(subjectActions.setLoading(true));
    try {
      const response = await API.subject.subjects(name);

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

export const getSubjectsNames = (): AsyncThunkType => {
  return async dispatch => {
    dispatch(subjectActions.setLoading(true));
    try {
      const response = await API.subject.subjectsNames();

      if (response) {
        dispatch(subjectActions.setSubjectsNames(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(subjectActions.setLoading(false));
    }
  }
};

export const createSubjects = (subjects: CreateSubjectType[]): AsyncThunkType => {
  return async dispatch => {
    dispatch(subjectActions.setLoading(true));
    try {
      const response = await API.subject.create(subjects);

      if (response) {
        dispatch(subjectActions.setError(''));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      dispatch(subjectActions.setError(error.message));
    } finally {
      dispatch(subjectActions.setLoading(false));
    }
  }
};

export const updateSubjectsNames = (oldName: string, newName: string): AsyncThunkType => {
  return async dispatch => {
    dispatch(subjectActions.setLoading(true));
    try {
      const response = await API.subject.updateName({
        name: oldName,
        newName: newName
      });

      if (response) {
        dispatch(subjectActions.setSubjects(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      dispatch(subjectActions.setError(error.message));
    } finally {
      dispatch(subjectActions.setLoading(false));
    }
  }
};

export const updateSubject = (
  id: string,
  subject: CreateSubjectType
): AsyncThunkType => {
  return async (dispatch, getState) => {
    dispatch(subjectActions.setLoading(true));
    try {
      const response = await API.subject.update(id, subject);

      if (response) {
        const state = getState().subject;
        const subjectIndex = state.subjects.indexOf(
          state.subjects.find(s => s.id === id)!
        );


        dispatch(subjectActions.setSubjects([
          ...state.subjects.map((s, i) => i === subjectIndex ? response : s)
        ]));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      dispatch(subjectActions.setError(error.message));
    } finally {
      dispatch(subjectActions.setLoading(false));
    }
  }
};

export const deleteSubject = (
  id: string
): AsyncThunkType => {
  return async (dispatch, getState) => {
    dispatch(subjectActions.setLoading(true));
    try {
      const response = await API.subject.delete(id);

      if (response) {
        const state = getState().subject;

        dispatch(subjectActions.setSubjects([
          ...state.subjects.filter(s => s.id !== id)
        ]));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      dispatch(subjectActions.setError(error.message));
    } finally {
      dispatch(subjectActions.setLoading(false));
    }
  }
};

export const deleteSubjectByName = (
  name: string
): AsyncThunkType => {
  return async (dispatch, getState) => {
    dispatch(subjectActions.setLoading(true));
    try {
      const response = await API.subject.deleteName({
        name: name
      });

      if (response) {
        dispatch(subjectActions.setError(''));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      dispatch(subjectActions.setError(error.message));
    } finally {
      dispatch(subjectActions.setLoading(false));
    }
  }
};

export const getAllSubjects = (): AsyncThunkType => {
  return async dispatch => {
    dispatch(subjectActions.setLoadingInitial(true));
    try {
      const response = await API.subject.allSubjects();

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
