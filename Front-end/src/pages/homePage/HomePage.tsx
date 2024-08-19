import { AppLists, ProjectList } from "../../components";
import { TaskList } from "../../components/tasks/TasksList";
import { MainLayout } from "../../layout/MainLayout";
import "bootstrap/dist/css/bootstrap.min.css";

export const HomePage = () => {
  return (
    <MainLayout>
      <h2 className="mb-4">Bem vindo ao gerenciador de tarefas</h2>
      <div className="container">
        <div className="row">
          <div className="col-md-6">
            <AppLists>
              <ProjectList />
            </AppLists>
          </div>
          <div className="col-md-6">
            <AppLists>
              <TaskList />
            </AppLists>
          </div>
        </div>
      </div>
    </MainLayout>
  );
};
