import { AppLists, ProjectList } from "../../components";
import { MainLayout } from "../../layout/MainLayout";

export const HomePage = () => {
  return (
    <>
      <MainLayout>
        <h2>Bem vindo ao gerenciador de tarefas</h2>
        <AppLists>
          <ProjectList></ProjectList>
        </AppLists>
      </MainLayout>
    </>
  );
};
