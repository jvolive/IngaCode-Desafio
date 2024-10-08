PGDMP      $                |            db_ingacode    16.4    16.4 "    E           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            F           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            G           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            H           1262    16394    db_ingacode    DATABASE     �   CREATE DATABASE db_ingacode WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Portuguese_Brazil.1252';
    DROP DATABASE db_ingacode;
                postgres    false                        3079    49152    pgcrypto 	   EXTENSION     <   CREATE EXTENSION IF NOT EXISTS pgcrypto WITH SCHEMA public;
    DROP EXTENSION pgcrypto;
                   false            I           0    0    EXTENSION pgcrypto    COMMENT     <   COMMENT ON EXTENSION pgcrypto IS 'cryptographic functions';
                        false    3                        3079    24576 	   uuid-ossp 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;
    DROP EXTENSION "uuid-ossp";
                   false            J           0    0    EXTENSION "uuid-ossp"    COMMENT     W   COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';
                        false    2            ~           1247    24590 
   dm_desc250    DOMAIN     ;   CREATE DOMAIN public.dm_desc250 AS character varying(250);
    DROP DOMAIN public.dm_desc250;
       public          postgres    false            �           1247    40961    dm_timestamp    DOMAIN     Y   CREATE DOMAIN public.dm_timestamp AS timestamp with time zone DEFAULT CURRENT_TIMESTAMP;
 !   DROP DOMAIN public.dm_timestamp;
       public          postgres    false                       1255    49189    hash_password(text)    FUNCTION     �   CREATE FUNCTION public.hash_password(password text) RETURNS text
    LANGUAGE plpgsql
    AS $$
BEGIN RETURN crypt(password, gen_salt('bf', 8));
END;
$$;
 3   DROP FUNCTION public.hash_password(password text);
       public          postgres    false                       1255    49190    verify_password(text, text)    FUNCTION     �   CREATE FUNCTION public.verify_password(stored_hash text, password text) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
BEGIN RETURN stored_hash = crypt(password, stored_hash);
END;
$$;
 G   DROP FUNCTION public.verify_password(stored_hash text, password text);
       public          postgres    false            �            1259    40972    collaborators    TABLE     &  CREATE TABLE public.collaborators (
    id_collab uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    name_collab public.dm_desc250 NOT NULL,
    user_id uuid,
    createdat_collab public.dm_timestamp,
    updatedat_collab public.dm_timestamp,
    deletedat_collab timestamp with time zone
);
 !   DROP TABLE public.collaborators;
       public         heap    postgres    false    2    897    897    894            �            1259    40985    projects    TABLE       CREATE TABLE public.projects (
    id_proj uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    name_proj public.dm_desc250 NOT NULL,
    createdat_proj public.dm_timestamp,
    updatedat_proj public.dm_timestamp,
    deletedat_proj timestamp with time zone
);
    DROP TABLE public.projects;
       public         heap    postgres    false    2    897    897    894            �            1259    40993    tasks    TABLE     R  CREATE TABLE public.tasks (
    id_task uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    name_task public.dm_desc250 NOT NULL,
    description_task text,
    proj_id uuid,
    createdat_task public.dm_timestamp,
    updatedat_task public.dm_timestamp,
    deletedat_task timestamp with time zone,
    collab_name public.dm_desc250
);
    DROP TABLE public.tasks;
       public         heap    postgres    false    2    897    894    894    897            �            1259    41006    time_tracker    TABLE     �  CREATE TABLE public.time_tracker (
    id_time_tracker uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    start_date_time_tracker timestamp with time zone NOT NULL,
    end_date_time_tracker timestamp with time zone NOT NULL,
    time_zone_id character varying(200) NOT NULL,
    task_id uuid,
    collab_id uuid,
    createdat_time_tracker public.dm_timestamp,
    updatedat_time_tracker public.dm_timestamp,
    deletedat_time_tracker timestamp with time zone
);
     DROP TABLE public.time_tracker;
       public         heap    postgres    false    2    897    897            �            1259    40962    users    TABLE     9  CREATE TABLE public.users (
    id_user uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    username_user public.dm_desc250 NOT NULL,
    password_user character varying(512) NOT NULL,
    createdat_user public.dm_timestamp,
    updatedat_user public.dm_timestamp,
    deletedat_user timestamp with time zone
);
    DROP TABLE public.users;
       public         heap    postgres    false    2    897    894    897            ?          0    40972    collaborators 
   TABLE DATA           ~   COPY public.collaborators (id_collab, name_collab, user_id, createdat_collab, updatedat_collab, deletedat_collab) FROM stdin;
    public          postgres    false    218   ,       @          0    40985    projects 
   TABLE DATA           f   COPY public.projects (id_proj, name_proj, createdat_proj, updatedat_proj, deletedat_proj) FROM stdin;
    public          postgres    false    219   �,       A          0    40993    tasks 
   TABLE DATA           �   COPY public.tasks (id_task, name_task, description_task, proj_id, createdat_task, updatedat_task, deletedat_task, collab_name) FROM stdin;
    public          postgres    false    220   /       B          0    41006    time_tracker 
   TABLE DATA           �   COPY public.time_tracker (id_time_tracker, start_date_time_tracker, end_date_time_tracker, time_zone_id, task_id, collab_id, createdat_time_tracker, updatedat_time_tracker, deletedat_time_tracker) FROM stdin;
    public          postgres    false    221   �1       >          0    40962    users 
   TABLE DATA           v   COPY public.users (id_user, username_user, password_user, createdat_user, updatedat_user, deletedat_user) FROM stdin;
    public          postgres    false    217   �3       �           2606    40979     collaborators collaborators_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public.collaborators
    ADD CONSTRAINT collaborators_pkey PRIMARY KEY (id_collab);
 J   ALTER TABLE ONLY public.collaborators DROP CONSTRAINT collaborators_pkey;
       public            postgres    false    218            �           2606    40992    projects projects_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.projects
    ADD CONSTRAINT projects_pkey PRIMARY KEY (id_proj);
 @   ALTER TABLE ONLY public.projects DROP CONSTRAINT projects_pkey;
       public            postgres    false    219            �           2606    41000    tasks tasks_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.tasks
    ADD CONSTRAINT tasks_pkey PRIMARY KEY (id_task);
 :   ALTER TABLE ONLY public.tasks DROP CONSTRAINT tasks_pkey;
       public            postgres    false    220            �           2606    41011    time_tracker time_tracker_pkey 
   CONSTRAINT     i   ALTER TABLE ONLY public.time_tracker
    ADD CONSTRAINT time_tracker_pkey PRIMARY KEY (id_time_tracker);
 H   ALTER TABLE ONLY public.time_tracker DROP CONSTRAINT time_tracker_pkey;
       public            postgres    false    221            �           2606    57347     collaborators unique_name_collab 
   CONSTRAINT     b   ALTER TABLE ONLY public.collaborators
    ADD CONSTRAINT unique_name_collab UNIQUE (name_collab);
 J   ALTER TABLE ONLY public.collaborators DROP CONSTRAINT unique_name_collab;
       public            postgres    false    218            �           2606    40969    users users_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id_user);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    217            �           2606    40971    users users_username_user_key 
   CONSTRAINT     a   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_user_key UNIQUE (username_user);
 G   ALTER TABLE ONLY public.users DROP CONSTRAINT users_username_user_key;
       public            postgres    false    217            �           2606    40980 (   collaborators collaborators_user_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.collaborators
    ADD CONSTRAINT collaborators_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id_user) ON DELETE CASCADE;
 R   ALTER TABLE ONLY public.collaborators DROP CONSTRAINT collaborators_user_id_fkey;
       public          postgres    false    4765    217    218            �           2606    57348    tasks tasks_collab_name_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.tasks
    ADD CONSTRAINT tasks_collab_name_fkey FOREIGN KEY (collab_name) REFERENCES public.collaborators(name_collab);
 F   ALTER TABLE ONLY public.tasks DROP CONSTRAINT tasks_collab_name_fkey;
       public          postgres    false    220    4771    218            �           2606    41001    tasks tasks_proj_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.tasks
    ADD CONSTRAINT tasks_proj_id_fkey FOREIGN KEY (proj_id) REFERENCES public.projects(id_proj) ON DELETE CASCADE;
 B   ALTER TABLE ONLY public.tasks DROP CONSTRAINT tasks_proj_id_fkey;
       public          postgres    false    219    4773    220            �           2606    41017 (   time_tracker time_tracker_collab_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.time_tracker
    ADD CONSTRAINT time_tracker_collab_id_fkey FOREIGN KEY (collab_id) REFERENCES public.collaborators(id_collab) ON DELETE SET NULL;
 R   ALTER TABLE ONLY public.time_tracker DROP CONSTRAINT time_tracker_collab_id_fkey;
       public          postgres    false    218    4769    221            �           2606    41012 &   time_tracker time_tracker_task_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.time_tracker
    ADD CONSTRAINT time_tracker_task_id_fkey FOREIGN KEY (task_id) REFERENCES public.tasks(id_task) ON DELETE CASCADE;
 P   ALTER TABLE ONLY public.time_tracker DROP CONSTRAINT time_tracker_task_id_fkey;
       public          postgres    false    221    4775    220            ?   r   x��̻�  ��L�κ/"�W����d��Oz3XB0��ht��t51㹍u߭��}ֳ�����R���:��
�����
X��Ez0��,.��=���SJ_*8$y      @   n  x���M��8��U�����(��r�E/��d	�Iw����>re*�v
���g��z�9�����"Pj/��S�f��O_^�����N������mM�dE�(G�W�%���~���>R��P(��ϻ�y�*Yk��`��D+ʒ8B�ĝU�UJ��CAK���:�#m��x����pռ�,��fG��+��ӿ8+��X��	���6 ������sx)����֘bՙ(/x��Q�����;8���P[��URi�ݰr�ǧ���������7�Lo%^,���}Nۨ[��y���C�9�R-�u�7���+_XV��l��3�w���z�c/%1NF{�J��6�
�@��<�Y����Ȥ|�b�]L��(�c�Ʈ�>Z�|-d�2U��h�2��YMk���s&�[!�&�k��N�v��Ȓi�^�P�˘o� M�Pqs�$q�棣�d��Yl^����O�ڐ�@:�6���`�5��R�1�^��^~���\�M�u�uY˘)�<ö���b��5k����w�e������}tN��矘�p��&�b��s�\Ps�lP
i�����п�^~M=�=$s�i�̘�H��'so�幧"g����|>�(L%      A   `  x����n9E��_�?��(RY����Ko���Ãv��QW;@�m� 	�PP�B�y�z�1�$��0�S����TZ������K8y��O�O�㹿�_qՠ����(�J��4�ļG�%��P�h�-�J������q������Tf`�Z֘�*��;mܿ�K==�s~z�-�Lޡ�``�ԤCm�	#i�"�lKyM٢���Vc��(�Ē#^ph	��4�I��R�q Zt�2W
�<�z������_$�#�t����%���J�f"���a�`f��)R��)sC��!Z.!$�.y������}����!$ϓZJ ���d�U��ky�s��6�5���.�L,L��U��Dh�^��%u����r)��7Q�E{��ׄ^�>�h[���)�=׭z����%y ��l����ƖD|>�;Q_~	7N+�R�w�n�+͈
G-�[K�<�[8z��[(r�㟦BȆ�a^�f�x�x�N������瓟�O��-��/���L'��Z6Cd���'�B���x�\�U�$�{�H�U՘nՐ�4��M"�N~�݊��|���cF�PL��εQMZ�� 3�dBr�{���>����_̸yC      B   2  x��Vۊ�@}�|���\�F3�[���P�3c(�,���7q�iLc��IG:��ܒK��WеfpvL2ҺVB�O����<c��������j��c����/������������Az!̰P�`9{%W_�<���;pZ����Ti��H����ϩ̒'.bdׄ��L��8�̪S�\$�[|qw�cT�^�� �b�j�=�Ձ�uH@��:l\4�N������{4�L:��e��0p0.5�SY"z|y��\�1���n�P��3)��BvLuG��t��V�S��.�c�[�������2<p,�T�'qU;������T�q�W�N+&(L��暪�R������l�����y�4�����A���v6�f��^��4E_��ư�o�!�� ��Ĵ����l�F�-m5E��5m��'1����uױ�8?T��Iʹ�=���3����z��`@	舭���r[M۰Qi��y�ߵ}3?@��gF<��T2%�-D���9�S�5�m�<p�?��t(�������$���I�a	m@b���]ؾL���7���      >   �   x��ɻ�0 Й~�k����1F4�����"��~����9��FS�rMe����g�W6z�~�b�1$�z�\]=��i]��+]}Xy���ڹ<޻��Ku+��ac��(#���1C��dR�?�.H�!_��+U     