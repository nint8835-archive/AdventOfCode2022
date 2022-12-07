from typing import Union, Iterator

Directory = dict[str, Union[int, "Directory"]]


def get_new_path(cwd: str, cd_arg: str) -> str:
    if cd_arg == "/":
        return cd_arg
    elif cd_arg == "..":
        return "/".join(cwd.split("/")[:-2]) + "/"
    else:
        return f"{cwd}{cd_arg}/"


class Filesystem:
    def __init__(self):
        self.contents: Directory = {}

    def get_dir(self, path: str) -> Directory:
        path_components = path.split("/")

        directory = self.contents

        for component in path_components:
            if component == "":
                continue

            if component not in directory:
                directory[component] = {}
            directory = directory[component]

        return directory

    def process_input(self, input: list[list[str]]) -> None:
        cwd = ""
        directory = {}
        for command_execution in input:
            command, *args = command_execution[0].split(" ")
            if command == "cd":
                cwd = get_new_path(cwd, args[0])
                directory = self.get_dir(cwd)
            elif command == "ls":
                for line in command_execution[1:]:
                    info, name = line.split(" ")
                    if info == "dir":
                        directory[name] = {}
                    else:
                        directory[name] = int(info)

    def get_directory_size(self, dir: Directory) -> int:
        size = 0
        for item in dir.values():
            if isinstance(item, int):
                size += item
            else:
                size += self.get_directory_size(item)
        return size

    def get_dir_names(self, cwd: str = "/") -> Iterator[str]:
        contents = self.get_dir(cwd)

        yield cwd

        for k, v in contents.items():
            if isinstance(v, dict):
                yield from self.get_dir_names(f"{cwd}{k}/")


with open("input.txt") as f:
    input = [
        command.strip().split("\n") for command in f.read().split("$") if command != ""
    ]

fs = Filesystem()
fs.process_input(input)

part_a = 0
for dir_name in fs.get_dir_names():
    dir_size = fs.get_directory_size(fs.get_dir(dir_name))
    if dir_size <= 100000:
        part_a += dir_size

print(part_a)

total_size = fs.get_directory_size(fs.get_dir("/"))
free_space = 70000000 - total_size
needed_delete = 30000000 - free_space
delete_candidates = []
for dir_name in fs.get_dir_names():
    dir_size = fs.get_directory_size(fs.get_dir(dir_name))
    if dir_size >= needed_delete:
        delete_candidates.append(dir_size)

print(sorted(delete_candidates)[0])
