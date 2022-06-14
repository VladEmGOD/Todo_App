import {gql} from "@apollo/client";

export const TODO_FRAGMENT = gql`
  fragment TodoFragment on TodoType {
    id,
    title,
    deadline,
    categoryId,
    isDone
  }
 `