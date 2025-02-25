import { ColorClassEnum, IconsEnum } from "src/app/services/enum-cluster.service";

export interface CardButtonModel {
    text: string;
    icon: IconsEnum | undefined;
    colorClass: ColorClassEnum;

    onClick: (identifier: string | undefined) => void;
}