import {ThunkAction} from "redux-thunk/es/types";
import {RootState} from "../index";
import {groupActions, GroupActionType} from "./index";
import {ServerErrorType} from "../../model/ServerError";
import {API} from "../../api";
import {
  CreateGroupStudentsType
} from "../../model/Group/CreateGroup";

type AsyncThunkType = ThunkAction<Promise<void>,
  RootState,
  unknown,
  GroupActionType>;

export const getGroups = (): AsyncThunkType => {
  return async dispatch => {
    dispatch(groupActions.setLoadingInitial(true));
    try {
      const response = await API.group.groups();

      if (response) {
        dispatch(groupActions.setGroups(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(groupActions.setLoadingInitial(false));
    }
  }
};

export const editStudents = (
  id: string,
  students: string[]
): AsyncThunkType => {
  return async dispatch => {
    dispatch(groupActions.setLoading(true));
    try {
      const response = await API.group.students(id, students);

      if (response) {
        dispatch(groupActions.setError(''));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(groupActions.setLoading(false));
    }
  }
};

export const deleteGroup = (
  id: string
): AsyncThunkType => {
  return async dispatch => {
    dispatch(groupActions.setLoading(true));
    try {
      const response = await API.group.deleteGroup(id);

      if (response) {
        dispatch(groupActions.setError(''));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(groupActions.setLoading(false));
    }
  }
};

export const createGroup = (
  payload: CreateGroupStudentsType
): AsyncThunkType => {
  return async dispatch => {
    dispatch(groupActions.setLoading(true));
    try {
      const response = await API.group.create({
        ...payload
      });

      if (response && payload.students.length > 0) {
        const responseStudents = await API.group.students(
          response.id,
          payload.students
        );

        if (responseStudents) {
          dispatch(groupActions.setError(''));
        }
      }else if (response) {
        dispatch(groupActions.setError(''));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(groupActions.setLoading(false));
    }
  }
};