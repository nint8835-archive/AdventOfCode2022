from pprint import pprint


def get_visible(grid: list[list[int]], direction: str) -> set[tuple[int, int]]:
    height = len(grid)
    width = len(grid[0])

    axis, start, stop, step, coord_getter = {
        "lr": ("row", 0, width, 1, lambda line, i: (i, line)),
        "rl": ("row", width - 1, -1, -1, lambda line, i: (i, line)),
        "td": ("column", 0, height, 1, lambda line, i: (line, i)),
        "bu": ("column", height - 1, -1, -1, lambda line, i: (line, i)),
    }[direction]

    if axis == "row":
        lines = grid
    else:
        lines = [[row[i] for row in grid] for i in range(width)]

    visible = set()
    for line_index, line in enumerate(lines):
        max = line[start]
        for i in range(start, stop, step):
            if i in [start, stop] or line_index in [0, len(line) - 1]:
                visible.add(coord_getter(line_index, i))
            val = line[i]
            if val > max:
                print(val, coord_getter(line_index, i))
                max = val
                visible.add(coord_getter(line_index, i))

    return visible


with open("input.txt") as f:
    grid = [[int(char) for char in line.strip()] for line in f]

visible_sets = [get_visible(grid, direction) for direction in ["lr", "rl", "td", "bu"]]
all_visible = set.union(*visible_sets)
print(len(all_visible))
# pprint(all_visible)
