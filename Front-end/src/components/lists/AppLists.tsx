import React from "react";
import ListGroup from "react-bootstrap/ListGroup";

interface AppListsProps {
  children: React.ReactNode;
}

export const AppLists: React.FC<AppListsProps> = ({ children }) => {
  return <ListGroup>{children}</ListGroup>;
};
